using M002.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M002.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		/// <summary>
		/// Dependency Injection: C# Objekte innerhalb der Controller angreifbar machen
		/// z.B.: Logger, DB, ...
		/// 
		/// In der Main-Methode müssen die Objekte die injected werden sollen angemeldet werden per Add... Methode
		/// In jedem Controller sind dann diese Objekte verfügbar
		/// </summary>
		public HomeController(ILogger<HomeController> logger, DependencyInjectionTest di)
		{
			_logger = logger;
			di.Counter++;
		}

		public IActionResult Index()
		{
			return View();
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
}
