using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shoppite.Core.Configuration;
using Shoppite.Core.Interfaces;
using Shoppite.Core.Repositories;
using Shoppite.Core.Repositories.Base;
using Shoppite.Infrastructure.Logging;
using Shoppite.Infrastructure.Repository;
using Shoppite.Infrastructure.Repository.Base;
using Shoppite.Web.HealthChecks;
using Shoppite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Shoppite.Application.Interfaces;
using Shoppite.Application.Services;
using Shoppite.UI.Interfaces;
using Shoppite.UI.Services;
using Shoppite.Application.Services;
using Shoppite.Application.Interfaces;
using Shoppite.Web.Interfaces;
using Shoppite.Web.Services;
using Microsoft.AspNetCore.Identity;
using Shoppite.UI.Extensions;
using System.Text;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Shoppite.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            ConfigureShoppiteServices(services);
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddHttpContextAccessor();
            services.Configure<HtmlHelperOptions>(o => o.ClientValidationEnabled = false);

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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy();
            app.UseSession();

            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("JWToken");

                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Authentications}/{action=LogIn}/{id?}");
            });
        }

        private void ConfigureShoppiteServices(IServiceCollection services)
        {
            // Add Core Layer
            services.Configure<ShoppiteSettings>(Configuration);

            // Add Infrastructure Layer
            ConfigureDatabases(services);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductDetailRepsitory, ProductDetailRepository>();
            services.AddScoped<IAuthenticationsRepository, AuthenticatiosRepository>();
            services.AddScoped<ICartRepository, CartRepository>();

            // Add Application Layer
            services.AddScoped<IBrandServices, BrandServices>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductDetailServices, ProductDetilServices>();
            services.AddScoped<IAuthenticationsService, AuthenticationsService>();
            services.AddScoped<ICartServices, CartServices>();

            // Add Web Layer
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IBrandPageServices, BrandPageServices>();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ICategoryPageService, CategoryPageService>();
            services.AddScoped<IproductDetailPageServices, ProductDetailPageServices>();
            services.AddScoped<ICartPageServices, CartPageServices>();
            services.AddScoped<IAuthenticationsPageService, AuthenticationPageService>();

            // Add Miscellaneous
            services.AddHttpContextAccessor();
            services.AddHealthChecks()
                .AddCheck<IndexPageHealthCheck>("home_page_health_check");
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<Shoppite_masterContext>();

            SiteKeys.Configure(Configuration.GetSection("AppSettings"));
            var key = Encoding.ASCII.GetBytes(SiteKeys.Token);

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(token =>
               {
                   token.RequireHttpsMetadata = false;
                   token.SaveToken = true;
                   token.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidateIssuer = true,
                       ValidIssuer = SiteKeys.WebSiteDomain,
                       ValidateAudience = true,
                       ValidAudience = SiteKeys.WebSiteDomain,
                       RequireExpirationTime = true,
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.Zero
                   };
               });
        }
        public void ConfigureDatabases(IServiceCollection services)
        {
            // use in-memory database
            //services.AddDbContext<ShoppiteContext>(c =>
            //    c.UseInMemoryDatabase("ShoppiteConnection"));

            //// use real database
            services.AddDbContext<Shoppite_masterContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("ShoppiteConnection")));
        }
    }
}
