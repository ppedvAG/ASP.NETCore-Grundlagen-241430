using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004.Pages.User
{
    public class LoginModel : PageModel
	{
		private List<UserModel> _users;

		/// <summary>
		/// Ausgaben in die Konsole machen mittels ILogger
		/// </summary>
		private ILogger<LoginModel> _logger;

		public LoginModel(ILogger<LoginModel> logger, List<UserModel> users)
		{
			_logger = logger;
			_users = users;
		}

		public IActionResult OnPost(string user, string pw)
		{
			UserModel? foundUser = _users.FirstOrDefault(e => e.Username == user);
			if (foundUser == null)
			{
				return BadRequest();
			}

			if (foundUser.Password != pw)
			{
				return Forbid();
			}

			_logger.Log(LogLevel.Information, $"User eingeloggt: {user}");

			//Hier anonymes Objekt einsetzen, wobei die Namen hierin mit den Parameternamen des Handlers anderen Seite übereinstimmen müssen
			return RedirectToPage("/User/Erfolg", new { user = foundUser.Username });
		}
	}
}
