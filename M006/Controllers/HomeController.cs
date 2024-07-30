using M006.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace M006.Controllers;

/// <summary>
/// EFCore
/// ORM, welcher eine einfache Datenbankanbindung erm�glicht
/// 
/// Vorteile:
/// - Direktes Mapping der DB zur Applikation
/// - Einfaches Angreifen der Daten
/// 
/// Nachteile:
/// - Keine einfachen Methoden um gro�e Datenmengen zu verarbeiten
/// - Probleme mit GroupBy
/// 
//////////////////////////////////////////////////////////////////
/// 
/// Pakete:
/// - Microsoft.EntityFrameworkCore
/// - Microsoft.EntityFrameworkCore.SqlServer
/// - Microsoft.EntityFrameworkCore.Design
/// - Microsoft.EntityFrameworkCore.Tools
/// 
/// VS Extension: EFCore Power Tools
/// 
/// Rechtsklick auf Projekt -> EFCore Power Tools -> Reverse Engineer
/// 
/// Verbindung herstellen -> Tabellen ausw�hlen -> Include connection string
/// 
//////////////////////////////////////////////////////////////////
///
/// Die Context klasse wird verwendet, um auf die Daten zuzugreifen
/// 
/// Per DI k�nnen wir die Kontextklasse in unseren Controllern verwendbar machen
/// 
/// F�r Gro�e Datenmengen: EFCore Bulk Extensions
/// https://github.com/borisdj/EFCore.BulkExtensions
/// 
/// </summary>
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
		//EF �bersetzt Linq Ketten zu SQL Statements
		//SELECT * FROM Customers WHERE CustomerId LIKE 'A%'
		//IEnumerable<Customer> customerMitA = _db.Customers.Where(e => e.CustomerId[0] == 'A');
		IEnumerable<Customer> customerMitA = _db.Customers.FromSqlRaw("SELECT * FROM Customers WHERE CustomerId LIKE 'A%'");

		//Achtung: ToList/AsEnumerable holen die Daten von der Datenbank
		//_db.Customers.ToList().Where(e => e.CustomerId[0] == 'A'); //Ab ToList wird das restliche Linq Lokal ausgef�hrt
		//customerMitA.ToList();

		return View(customerMitA);
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
