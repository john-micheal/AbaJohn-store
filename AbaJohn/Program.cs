using AbaJohn.Models;
using AbaJohn.Services.AccountRepository;
using AbaJohn;
using AbaJohn.Services;
using AbaJohn.Services.user;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using AbaJohn.Services.Itemss;

namespace AbaJohn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Localization  مكان المرادفعات او المعاني للكيمات 

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resource");
            builder.Services.AddMvc()
                .AddViewLocalization(options => options.ResourcesPath = "Resource")
                .AddDataAnnotationsLocalization();
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                 {
                        new CultureInfo("en-US"),
                        new CultureInfo("ar"),
       /*                 new CultureInfo("de"),
                        new CultureInfo("fr"),
                        new CultureInfo("es"),*/
                    };
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });


       

            // configer the _context 
            /*            builder.Services.AddTransient<ITIEntites, ITIEntites>();*/
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer("Data Source=.;Initial Catalog=AbaJohn;Integrated Security=True"));


           // inject usermanger  -  singInmanager 
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddScoped<IcategoeryRepository, categoeryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<Iuser, UserRepository>();
            builder.Services.AddScoped<IItem, ItemRepository>();
            builder.Services.AddAutoMapper(typeof(Program));
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


            var options = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            //  add this here to injuct  Authentication
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}