using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004.Pages;

public class IndexModel : PageModel
{
	private readonly ILogger<IndexModel> _logger;

	public int Zahl;

	public int? ID;

	public IndexModel(ILogger<IndexModel> logger)
	{
		_logger = logger;
	}

	/// <summary>
	/// Die On Metoden: Die Actions (hier Handler genannt), welche Code ausführen können
	/// z.B.: OnGet, OnPost, OnDelete, ...
	/// 
	/// Wenn über die URL ein Parameter kommt, kann dieser hier empfangen werden
	/// 
	/// Die On Methoden benötigen kein IActionResult, kann aber implementiert werden
	/// </summary>
	public IActionResult OnGet(int? id)
	{
		//In der View gibt es einen direkten Zugriff auf die Zahl
		Zahl = Random.Shared.Next();

		ID ??= id; //??-Operator: Nimm die linke Seite wenn sie nicht null ist, nimm die rechte Seite wenn die linke Seite null ist

		return Page(); //Page() = View()
	}
}