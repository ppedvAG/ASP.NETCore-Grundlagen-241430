namespace M001;

public class Startup
{
	public void ConfigureServices(IServiceCollection collection)
	{
		collection.AddSingleton<String>();
	}

	public void ConfigureMiddleware(WebApplicationBuilder builder, WebApplication app)
	{
		app.UseStaticFiles();
		app.MapGet("/Test", () => "Das ist ein Test");
	}
}
