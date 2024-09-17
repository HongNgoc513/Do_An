using DoAnWeb;
using DoAnWeb.ThanhToan;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme
).AddCookie(s =>
{
    s.Cookie.HttpOnly = false; // Không thiết lập thuộc tính HttpOnly
    s.Cookie.Name = "CookiesLogin"; // Đặt tên cho cookie là "MyCustomCookieName"
    s.LoginPath = "/account/login";
    s.LogoutPath = "/account/logout";
});

// Đăng ký IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSession();

ConfigureServices(builder.Services);
builder.Services.AddScoped<IVnPayService, VnPayService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=Index}/{id?}");

app.Run();


void ConfigureServices(IServiceCollection services)
{
    services.AddRazorPages(options =>
    {
        options.Conventions.AddAreaPageRoute("Admin", "/Home", "/Admin/Home");
    });
}