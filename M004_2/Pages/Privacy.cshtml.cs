using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_2.Pages;

public class PrivacyModel : PageModel
{
	private readonly ILogger<PrivacyModel> _logger;

	public int? AnzZahlen { get; set; }
	
	public PrivacyModel(ILogger<PrivacyModel> logger)
	{
		_logger = logger;
	}

	public void OnGet(int? anzZahlen)
	{
		AnzZahlen = anzZahlen;
	}

	public IActionResult OnPost(string[] zahlen, Operation op)
	{
		double[] d = new double[zahlen.Length];
		for (int i = 0; i < zahlen.Length; i++)
		{
			if (double.TryParse(zahlen[i], out double z))
			{
				d[i] = z;
			}
			else
			{
				return BadRequest();
			}
		}

		double ergebnis = d[0];
		foreach (double zahl in d.Skip(1))
		{
			switch (op)
			{
				case Operation.Add:
					ergebnis += zahl;
					break;
				case Operation.Sub:
					ergebnis -= zahl;
					break;
				case Operation.Mult:
					ergebnis *= zahl;
					break;
				case Operation.Div:
					ergebnis /= zahl;
					break;
			}
		}
		return RedirectToPage("Ergebnis", new { d = ergebnis });
	}
}
