using Microsoft.EntityFrameworkCore;
using System.Net.Security;
using CalculadoraMVC.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using CalculadoraMVC.Repository;

namespace CalculadoraMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        private IConfiguration _Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlite(_Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ILoginRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IOperacionRepository, OperacionRepository>();
            services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
         .AddCookie(options => {
             options.LoginPath = "/Login/Index";
         });

            services.AddMvc(options => options.EnableEndpointRouting = false);


        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "CalculadoraMVCRoute",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Login", action = "Index" },
                    constraints: new { id = "[0-9]+" });
            });
        }
    }
}
