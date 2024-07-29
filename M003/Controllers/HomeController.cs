using M003.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M003.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	/// <summary>
	/// Jeder Controller hat im View Ordner einen eigenen Unterordner (hier Home)
	/// </summary>
	/// <param name="logger"></param>
	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	/// <summary>
	/// Jede View wird über die entsprechende Methode angesprochen mittels View()
	/// 
	/// Der Methodenname dieser Methode bestimmt die unterliegende View
	/// </summary>
	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	/// <summary>
	/// Neue View erstellen:
	/// - Rechtsklick auf Methodenname -> Add View
	/// - Rechtsklick auf den Ordner -> Add -> View
	/// </summary>
	public IActionResult Hallo()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
