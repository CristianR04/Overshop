using Overshop.Models;
using Overshop.Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddTransient<productoSeleccionado>();
builder.Services.AddTransient<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddTransient<IRepositorioCompras, RepositorioCompras>();
builder.Services.AddTransient<Irepositorioproveedor, Repositorioproveedor>();
builder.Services.AddTransient<IRepositorioHome, RepositorioHome>();
builder.Services.AddTransient<IRepositoriocarrito, Repositoriocarrito>();
builder.Services.AddTransient<IRepositorioPDF, RepositorioPDF>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    ;
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=menu}/{id?}");

app.Run();
