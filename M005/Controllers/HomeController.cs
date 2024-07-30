using M005.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M005.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public string Username = "Lukas";
		private string Passwort = "123";

		public bool IstEingeloggt;
			
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Index()
		{
			//HttpContext
			//Enthält Informationen zu verschiedenen Dingen zu dem derzeitigen Request/Response
			
			//HttpContext.Request
			//Enthält verschiedene Informationen zum Request (Header, Query, ...)

			//HttpContext.Response
			//Enthält u.a. Cookies

			if (HttpContext.Request.Cookies["loginToken"] != null)
			{
				IstEingeloggt = true;

				CookieOptions co = new CookieOptions();
				co.Expires = DateTimeOffset.Now + TimeSpan.FromDays(3);

				HttpContext.RenewCookie("loginToken", co);
				HttpContext.RenewCookie("eingeloggtBleiben", co);

				//CookieExtensions.RenewCookie(HttpContext, "loginToken", co);
			}

			return View((IstEingeloggt, Username));
		}

		//Die Http Methoden Attribute
		//Legt fest, das diese Methode nur per POST angesprochen werden kann
		//Existiert für jede Methode
		[HttpPost]
		public IActionResult Index(string user, string pw, bool eingeloggtBleiben)
		{
			if (Username == user && Passwort == pw)
			{
				CookieOptions co = new CookieOptions();
				co.Expires = DateTimeOffset.Now + TimeSpan.FromDays(3);

				HttpContext.Response.Cookies.Append("eingeloggtBleiben", eingeloggtBleiben.ToString(), co);
				HttpContext.Response.Cookies.Append("loginToken", user + ";" + pw, co);
				IstEingeloggt = true;
			}
			return View((IstEingeloggt, Username));
		}

		[HttpPost]
		public IActionResult FileUpload(IFormFile file)
		{
			using StreamWriter sw = new StreamWriter("Test.txt");
			file.CopyTo(sw.BaseStream);
			sw.Flush();
			
			return View("Index");
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

public static class CookieExtensions
{
	public static void RenewCookie(this HttpContext c, string key, CookieOptions co = null)
	{
		string cookie = c.Request.Cookies[key];
		c.Response.Cookies.Delete(key);
		c.Response.Cookies.Append(key, cookie, co);
	}
}