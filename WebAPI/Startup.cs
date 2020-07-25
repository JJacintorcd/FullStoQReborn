using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using WebAPI.Options;
using Microsoft.EntityFrameworkCore;
using Recodme.RD.FullStoQReborn.DataLayer.Person;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Recodme.RD.FullStoQReborn.BusinessLayer.Person;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Seeders;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace WebAPI
{
    public class Startup
    {
        

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ContextSeeder.Seed();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //Políticas de Cookies
            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            //Definição da identidade
            services.AddIdentity<FullStoqUser, FullStoqRole>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<Context>();

            //Definição da base de dados para autenticação e autorização
            services.AddDbContext<Context>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("Default"));
                });
            //Swagger Generator
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo() { Title = "My API", Version = "v1" });
            });

            //CORS irresponsável
            services.AddCors(x => x.AddPolicy("Default", (builder) => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer((options) =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };

                });
            
            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/Commercial/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/EssentialGoods/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/Person/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/Queue/{1}/{0}" + RazorViewEngine.ViewExtension);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options => options.RouteTemplate = swaggerOptions.JsonRoute);
            app.UseSwaggerUI(options => options.SwaggerEndpoint(swaggerOptions.UiEndpoint,
                swaggerOptions.ApiDescription));

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("GeneralPolicy");

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                   name: "api",
                   pattern: "api/{controller=Home}/{action=Index}/{id?}");
            });

            //var enUsCulture = new CultureInfo("en-US");
            //var localizationOptions = new RequestLocalizationOptions()
            //{
            //    SupportedCultures = new List<CultureInfo>()
            //    {
            //        enUsCulture
            //    },
            //    SupportedUICultures = new List<CultureInfo>()
            //    {
            //        enUsCulture
            //    },
            //    DefaultRequestCulture = new RequestCulture(enUsCulture),
            //    FallBackToParentCultures = false,
            //    FallBackToParentUICultures = false,
            //    RequestCultureProviders = null
            //};
            //app.UseRequestLocalization(localizationOptions);
        }
        public void SetupRolesAndUsers(UserManager<FullStoqUser> userManager, RoleManager<FullStoqRole> roleManager)
        {
            if (roleManager.FindByNameAsync("Client").Result == null) roleManager.CreateAsync(new FullStoqRole() { Name = "Client" }).Wait();
            if (roleManager.FindByNameAsync("Staff").Result == null) roleManager.CreateAsync(new FullStoqRole() { Name = "Staff" }).Wait();
            if (roleManager.FindByNameAsync("Security").Result == null) roleManager.CreateAsync(new FullStoqRole() { Name = "Security" }).Wait();
            if (roleManager.FindByNameAsync("Management").Result == null) roleManager.CreateAsync(new FullStoqRole() { Name = "Management" }).Wait();
            if (roleManager.FindByNameAsync("Admin").Result == null) roleManager.CreateAsync(new FullStoqRole() { Name = "Admin" }).Wait();
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var person = new Profile(912345678, "Utilizador", "Exemplo", 0000000, DateTime.Now);
                var abo = new AccountBusinessController(userManager, roleManager);
                var res = abo.Register("admin", "admin@restLen.com", "Admin123!#", person, "Admin").Result;
                var roleRes = userManager.AddToRoleAsync(userManager.FindByNameAsync("admin").Result, "Admin");
            }

        }
    }
}
