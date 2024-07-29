using M003_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace M003_2.Controllers;

public class CalculatorController : Controller
{
	public IActionResult Index()
	{
		return View();
	}


	public IActionResult Berechne(Operation rechenart, int z1, int z2)
	{
		return View("Ergebnis", rechenart switch
		{
			Operation.Add => z1 + z2,
			Operation.Sub => z1 - z2,
			Operation.Mult => z1 * z2,
			Operation.Div => (double) z1 / z2
		});
	}
}
