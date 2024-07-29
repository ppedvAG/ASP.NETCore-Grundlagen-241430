//wwwroot: Statische Dateien (CSS, JS, ...)
//Controllers: C# Logik zu den Pages (Großteil der Logik)
//Views: HTML Code zu den einzelnen Pages
//Controller und View sind verbunden miteinander (über View(), Model)
//Model: C# Klassen, welche die Kommunikation zw. Backend und Frontend ermöglicht

using M002;

//Dependency Injection
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<DependencyInjectionTest>(); //AddSingleton: Ein globales Objekt für alle User/Controller
builder.Services.AddTransient<DependencyInjectionTest>(); //AddTransient: Ein Objekt pro User/Session
builder.Services.AddScoped<DependencyInjectionTest>(); //AddSingleton: Ein Objekt pro HTTP Request

var app = builder.Build();

//Middleware
//Bei Request/Response wird der gesamte Pfad der Middleware Komponenten durchgegangen

if (!app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	//app.UseStatusCodePages(); //Zeigt eine fast leere Page an mit dem Status Code
	app.UseExceptionHandler("/Home/Error"); //Kann nicht gleichzeitig mit UseStatusCodePages verwendet werden
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //Wer ist der User?
app.UseAuthorization(); //Was darf der User?

app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action}/{id?}");

app.Run();