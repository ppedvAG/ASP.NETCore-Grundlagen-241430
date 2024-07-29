using M003.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M003.Controllers;

/// <summary>
/// Aufgabenstellung: Simples Loginportal
/// - Registrierung Page
/// - Login Page
/// 
/// Schritte:
/// - Controller erstellen
/// - Views zu den Pages
/// - Daten speichern (innerhalb einer List die von DI kommt)
/// -- Registrierung Page
/// -- Login Page
/// </summary>
public class UserController : Controller
{
	private List<User> _users;

	/// <summary>
	/// Ausgaben in die Konsole machen mittels ILogger
	/// </summary>
	private ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, List<User> users)
    {
		_logger = logger;
		_users = users;
    }

	/// <summary>
	/// Model: Beliebiges C# Objekt, welches Kommunikation zwischen Backend und GUI ermöglicht
	/// </summary>
    public IActionResult Register()
	{
		return View(); //Hier können per Parameter an die View beliebige Daten weitergegeben werden
	}

	public IActionResult Login()
	{
		return View();
	}

	/// <summary>
	/// Effekt: Neuer User mit UserName, Passwort, Email anlegen
	/// User Objekt erstellen und mit den eingegebenen Werten füllen
	/// Die eingebenenen Werte sind in der currentUser Variable zu finden
	/// 
	/// IActionResult: Beliebiger HTTP Code
	/// - BadRequest()
	/// - View()
	/// - Redirect()
	/// - StatusCode()
	/// </summary>
	public IActionResult NeuerUser(string email, string username, string passwort)
	{
		//Wenn ein Benutzername oder eine Email mit dem neuen User übereinstimmt
		User newUser = new User() { Email = email, Username = username, Password = passwort };

		if (_users.Any(e => e.Email == newUser.Email) || _users.Any(e => e.Username == newUser.Username))
		{
			return BadRequest();
		}

		_users.Add(newUser);
		_logger.Log(LogLevel.Information, $"Neuer User angelegt: {email}, {username}");
		return RedirectToAction("Login");
		//return StatusCode(200);
	}

	public IActionResult Einloggen(string user, string pw)
	{
		User? foundUser = _users.FirstOrDefault(e => e.Username == user);
		if (foundUser == null)
		{
			return BadRequest();
		}

		if (foundUser.Password != pw)
		{
			return Forbid();
		}

		_logger.Log(LogLevel.Information, $"User eingeloggt: {user}");
		return View("Erfolg", foundUser);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
