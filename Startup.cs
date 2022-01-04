using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using BusinessAnalytic.Models.Concrete.EfCore.UnitOfWork;

namespace BusinessAnalytic
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
            services.AddDbContext<AdventureWorks2019Context>(
       options => options.UseSqlServer("Data Source=tcp:businessanalyticdbserver.database.windows.net,1433;Initial Catalog=AdventureWorks2019;User Id=businessanalytic@businessanalyticdbserver;Password=123QWEasdzxc"));

            services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x => {
                    x.LoginPath = "/Login/Login/";
            });
            services.AddScoped<IProductRepository, EfCoreProductRepository>();
            services.AddScoped<IProductModelsRepository, EfCoreProductModelRepository>();
            services.AddScoped<IProductSubCategoriesRepository, EfCoreProductSubCategoriesRepository>();
            services.AddScoped<IUnitMeasuresRepository, EfCoreUnitMeasuresRepository>();
            services.AddScoped<IEfCoreUnitOfWork, EfCoreUnitOfWork>();
            services.AddScoped<ICustomersRepository, EfCoreCustomerRepository>();
            services.AddScoped<IEmployeeRepository, EfCoreEmployeeRepository>();
            services.AddScoped<ILocationsRepository, EfCoreLocationRepository>();
            services.AddScoped<IPeopleRepository, EfCorePeopleRepository>();
            services.AddScoped<IProductPurchaseRepository, EfCoreProductPurchaseRepository>();
            services.AddScoped<IProductSalesRepository, EfCoreProductSaleRepository>();
            services.AddScoped<IProductTransactionRepository, EfCoreProductTransactionsRepository>();
            services.AddScoped<IProfitRepository, EfCoreProfitRepository>();
            services.AddScoped<ISalesTerritoriesRepository, EfCoreSalesTerritoriesRepository>();
            services.AddScoped<IVendorsRepository, EfCoreVendorsRepository>();
            services.AddScoped<IWarehousesRepository, EfCoreWarehouseRepository>();

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
