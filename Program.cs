using Microsoft.EntityFrameworkCore;
using SistemaDeEncomendas.Models;
using Microsoft.AspNetCore.Localization;
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
            //var localOptions = new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture(cultureInfo),
            //    SupportedCultures = new List<CultureInfo> { cultureInfo },
            //    SupportedUICultures = new List<CultureInfo> { cultureInfo }
            //};

            //app.UseRequestLocalization(localOptions);

            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = ",";
            cultureInfo.NumberFormat.NumberDecimalDigits = 2;
            cultureInfo.NumberFormat.CurrencyDecimalDigits = 2;
            //cultureInfo.NumberFormat.NumberGroupSeparator = ".";
            //cultureInfo.NumberFormat.CurrencyGroupSeparator = ".";
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            //fim da correção do idioma...

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Encomendas}/{action=Index}/{id?}");

            app.Run();
        }
    }
}