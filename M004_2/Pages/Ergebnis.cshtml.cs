using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_2.Pages;

public class ErgebnisModel : PageModel
{
	public double Ergebnis { get; set; }

	public void OnGet(double d)
	{
		Ergebnis = d;
	}
}
