using Microsoft.AspNetCore.Mvc;

namespace M001;

public class Program
{
	public static void Main(string[] args)
	{
		//Dependency Injection
		Startup s = new Startup();
		var builder = WebApplication.CreateBuilder(args);
		s.ConfigureServices(builder.Services);
		var app = builder.Build();
		s.ConfigureMiddleware(builder, app);
		//Middleware

		//Map
		//Routen definieren mit Pfad und Methodenzeiger
		app.MapGet("/", () => "Hello World!"); //Anonyme Methode
		app.MapGet("/Hello", Hello); //Dedizierte Methode

		//Map mit Parameter
		app.MapGet("/Name/{name}", (string name) => $"Dein Name ist {name}"); //Anonyme Methode mit Parameter
		app.MapGet("/Name", Name); //Dedizierte Methode mit Parameter

		app.Run();
	}

	public static string Hello()
	{
		return "Hallo";
	}

	public static string Name(string name)
	{
		return $"Dein Name ist {name}";
	}
}
