using M000.Models;
using M006_2.Models;
using M006_Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M006_2.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly NorthwindContext _db;

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

		public IActionResult Customers()
		{
			return View(_db.Customers);
		}

		public IActionResult BestellungenAnzeigen(string id)
		{
			return View("BestellungenKunde", _db.Orders.Where(e => e.CustomerId == id));
		}

		public IActionResult DetailsAnzeigen(int id)
		{
			var x = _db.OrderDetails.Where(e => e.OrderId == id).Join
				(
					_db.Products, //Zweite Tabelle für den Join angeben
					od => od.ProductId, //Key von Tabelle 1
					p => p.ProductId, //Key von Tabelle 2
					(od, p) => new Rechnungsposten() { ProductId = od.ProductId, ProductName = p.ProductName, UnitPrice = od.UnitPrice, Quantity = od.Quantity, Price = od.UnitPrice * od.Quantity } //Die Form des Ergebnisses
				);
			return View("BestellungsDetails", x);
		}

		public IActionResult MitarbeiterAnzeigen(int id)
		{
			return View("MitarbeiterAnzeigen", _db.Employees.First(e => e.EmployeeId == id));
		}

		public IActionResult CustomerByCountry(string land)
		{
			return View("Customers", _db.Customers.Where(e => e.Country == land));
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
