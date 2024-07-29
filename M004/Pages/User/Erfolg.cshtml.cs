using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004.Pages.User;

public class ErfolgModel : PageModel
{
	public string CurrentUser;

	public void OnGet(string user)
	{
		CurrentUser = user;
	}
}
