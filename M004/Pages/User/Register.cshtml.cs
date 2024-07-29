using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using M004;

namespace M004.Pages.User;

public class RegisterModel : PageModel
{
	private List<UserModel> _users;

	/// <summary>
	/// Ausgaben in die Konsole machen mittels ILogger
	/// </summary>
	private ILogger<RegisterModel> _logger;

	public RegisterModel(ILogger<RegisterModel> logger, List<UserModel> users)
	{
		_logger = logger;
		_users = users;
	}

	public IActionResult OnPostNeuerUser(string email, string username, string passwort)
	{
		//Wenn ein Benutzername oder eine Email mit dem neuen User übereinstimmt
		UserModel newUser = new UserModel() { Email = email, Username = username, Password = passwort };

		if (_users.Any(e => e.Email == newUser.Email) || _users.Any(e => e.Username == newUser.Username))
		{
			return BadRequest();
		}

		_users.Add(newUser);
		_logger.Log(LogLevel.Information, $"Neuer User angelegt: {email}, {username}");
		return RedirectToPage("/User/Login");
	}
}