using M006_Data;
using M007.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices.ObjectiveC;

namespace M007.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly NorthwindContext _db;

	public IEnumerable<object> Data { get; set; }

	public HomeController(ILogger<HomeController> logger, NorthwindContext db)
	{
		_logger = logger;
		_db = db;
	}

	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	public IActionResult OrdersView()
	{
		return View(_db.Orders);
	}

	public IActionResult ProdukteAnzeigen(int id)
	{
		var x = _db.OrderDetails.Where(e => e.OrderId == id).Join
			(
				_db.Products, //Zweite Tabelle für den Join angeben
				od => od.ProductId, //Key von Tabelle 1
				p => p.ProductId, //Key von Tabelle 2
				(od, p) => new object[] { od.ProductId, p.ProductName, od.UnitPrice, od.Quantity, od.UnitPrice * od.Quantity } //Die Form des Ergebnisses
			);
		return View("ShowAnyData", x);
	}

	public IActionResult ShowAllCustomers()
	{
		Data = _db.Customers.ToList();
		TempData["Data"] = Data;
		return View("ShowAnyData", Data);
	}

	[HttpPost]
	public IActionResult Filter(string col, string expr)
	{
		IEnumerable<object> data = (IEnumerable<object>) TempData["Data"];

		PropertyInfo foundColumn = 
			data.GetType()
			.GetGenericArguments()[0]
			.ReflectedType
			.GetProperties()
			.FirstOrDefault(e => e.Name == col);

		if (foundColumn == null)
		{
			return NotFound();
		}

		List<object> filteredData = [];
		foreach (object o in Data)
		{
			if (o.GetType()
				.GetProperties()
				.FirstOrDefault(e => e.Name == foundColumn.Name)
				.GetValue(o)
				.ToString() == expr)
			{
				filteredData.Add(o);
			}
		}
		return View(filteredData);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
