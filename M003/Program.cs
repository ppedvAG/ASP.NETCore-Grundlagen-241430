using M003.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); //MVC aktivieren
builder.Services.AddSingleton<List<User>>(); //"Userdatenbank" per DI hinzufügen

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Controller Routen definieren
app.MapControllerRoute(
	name: "default",
	pattern: "/{controller=Home}/{action=Index}"); //Allgemeine Route für alle Controller

app.Run();
