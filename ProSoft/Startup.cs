using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Ef;
using DataAccess.Ef.Dto;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProSoft.Models;
using Services;

namespace ProSoft
{
    public class Startup
    {
        private string _contentRootPath = "";

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _contentRootPath = env.ContentRootPath;

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            string connectionString = Configuration.GetConnectionString("ProSoftDb");
            if (connectionString.Contains("%CONTENTROOTPATH%"))
                connectionString = connectionString.Replace("%CONTENTROOTPATH%", _contentRootPath);
            services.AddDbContext<ProSoftDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IGenUoW, EfUnit>();
            services.AddScoped<IDataProvider, DataProvider>();

            TypeAdapterConfig<Apartment, ApartmentViewModel>
                .NewConfig()
                .Map(d => d.ApartmentId, s => s.Id)
                .Map(d => d.ApartmentName, s => s.Name.Replace('/', ','))
                .Map(d => d.MeterId, s => s.Meterid)
                .Map(d => d.MeterNumber, s => s.Meter.Number)
                .Map(d => d.LastIndicationDateValue, s => s.Meter.Indication.Any() ? s.Meter.Indication.First().Datevalue.ToString("dd.MM.yyyy") : "")
                .Map(d => d.LastIndicationValue, s => s.Meter.Indication.Any() ? (int?)s.Meter.Indication.First().Value : null)
                .Map(d => d.LastVerificationDate, s => s.Meter.Lastverification.ToString("dd.MM.yyyy"))
                .Map(d => d.NextVerificationDate, s => s.Meter.Nextverification.ToString("dd.MM.yyyy"))
                .Map(d => d.LastIndicationId, s => s.Meter.Indication.Any() ? (int?)s.Meter.Indication.First().Id : null);

            TypeAdapterConfig<Address, AddressViewModel>
                .NewConfig()
                .Map(d => d.Address, s => $"{s.Street},{s.Building}");

            TypeAdapterConfig<Apartment, MeterViewModel>
                .NewConfig()
                .Map(d => d.ApartmentName, s => s.Name.Replace('/', ','))
                .Map(d => d.MeterNumber, s => s.Meter.Number)
                .Map(d => d.NextVerificationDate, s => s.Meter.Nextverification.ToString("dd.MM.yyyy"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
