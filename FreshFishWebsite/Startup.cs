using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Interfaces.Registering;
using FreshFishWebsite.Models;
using FreshFishWebsite.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FreshFishWebsite
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
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IStorageRepository, StorageRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPoolRepository, PoolRepository>();
            services.AddScoped<IProductInPoolRepository, ProductInPoolRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddDbContext<FreshFishDbContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

            })
            .AddEntityFrameworkStores<FreshFishDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthorization(options => {
                options.AddPolicy("MainAdmin",
                    policy => policy.RequireClaim("MainManager"));

                options.AddPolicy("AdminAssistant",
                    policy => policy.RequireClaim("ManagerAssistant"));
                });

            services.AddControllersWithViews();


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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
