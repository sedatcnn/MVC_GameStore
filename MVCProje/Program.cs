    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MVCProje.Data;
    using MVCProje.Repositories.Abstract;
    using MVCProje.Repositories.Implementation;
using MVCProje.ViewComponents;
using System.Security.Policy;

namespace MVCProje
    {
        public class Program
        {
            public static async Task Main(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

                builder.Services.AddScoped<IKategoriService, KategoriService>();
                builder.Services.AddScoped<ITurService, TurService>();
                builder.Services.AddScoped<IOyunlarService, OyunlarService>();
                builder.Services.AddScoped<ISepet, SepetService>();
                builder.Services.AddDatabaseDeveloperPageExceptionFilter();

                builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI().AddDefaultTokenProviders();

                builder.Services.AddControllersWithViews();

               

            var app = builder.Build();
                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseRouting();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapControllerRoute(
                        name: "areas",
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                      );
                    app.MapRazorPages();


                });
              
                app.Run();
            }
        }
    }
