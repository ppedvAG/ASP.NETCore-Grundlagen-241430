using M006_Data;
using M008.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace M008.Controllers;

//BindProperties: Bindet alle Properties innahlb des Controllers
//Mit From-Attributen einschränken
[BindProperties(SupportsGet = true)]
public class HomeController : Controller
{
	//BindProperty: Globales Property im Controller, welches Daten über die Route empfängt
	//localhost/Home/Index?Test=abc
	//localhost/Home/Privacy?Test=abc
	[BindProperty(SupportsGet = true)]
	[FromQuery] //From-Attribute: Definieren, wie dieses Feld befüllt werden kann
	public string Test { get; set; } 

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

	public IActionResult KundeBearbeiten(string id)
	{
		Customer c = _db.Customers.SingleOrDefault(e => e.CustomerId == id);
		if (c == null)
			return NotFound();

		TempData["currentCustomer"] = JsonSerializer.Serialize(c);
		return View("EditCustomer", c);
	}

	[HttpPost]
	public IActionResult KundeSpeichern([FromForm] Customer c)
	{
		//ModelState.IsValid: Prüft alle Validierungen Serverseitig
		//Schaut in das c Objekt, und prüft die DataAnnotations

		//if (!ModelState.IsValid)
		//{
		//	return BadRequest();
		//}
		
		Customer old = JsonSerializer.Deserialize<Customer>(TempData["currentCustomer"].ToString());
		_db.Remove(old);
		//...
		_db.Update(c);
		_db.SaveChanges();
		return View("Customers", _db.Customers);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
