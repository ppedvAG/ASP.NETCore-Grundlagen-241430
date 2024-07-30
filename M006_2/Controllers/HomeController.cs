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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
