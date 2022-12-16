using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using SistemaDeEncomendas.Models;
using System.Globalization;

namespace SistemaDeEncomendas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<Context>(context =>
            {
                context.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Authentication builder.
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = "/Usuario/LoginPage");

            builder.Services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //correção do idioma...
            var cultureInfo = new CultureInfo("pt-BR");

            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = ",";
            cultureInfo.NumberFormat.NumberDecimalDigits = 2;
            cultureInfo.NumberFormat.CurrencyDecimalDigits = 2;

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            //fim da correção do idioma...

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Encomendas}/{action=Index}/{id?}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app authentication
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always
            });

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseCookiePolicy();

            app.Run();
        }
    }
}