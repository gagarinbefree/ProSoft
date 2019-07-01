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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProSoft.Models;
using Services;

namespace ProSoft
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<ProSoftDbContext>();
            services.AddScoped<IGenUoW, EfUnit>();
            services.AddScoped<IDataProvider, DataProvider>();

            TypeAdapterConfig<Apartment, ApartmentModel>
                .NewConfig()
                .Map(d => d.ApartmentId, s => s.Id)
                .Map(d => d.ApartmentName, s => s.Name.Replace('/', ','))
                .Map(d => d.MeterId, s => s.Meterid)
                .Map(d => d.LastIndicationDateValue, s => s.Meter.Indication.Any() ? s.Meter.Indication.First().Datevalue.ToString("dd.MM.yyyy") : "")
                .Map(d => d.LastIndicationValue, s => s.Meter.Indication.Any() ? (int?)s.Meter.Indication.First().Value : null)
                .Map(d => d.LastVerificationDate, s => s.Meter.Lastverification.ToString("dd.MM.yyyy"))
                .Map(d => d.NextVerificationDate, s => s.Meter.Nextverification.ToString("dd.MM.yyyy"))
                .Map(d => d.LastIndicationId, s => s.Meter.Indication.Any() ? (int?)s.Meter.Indication.First().Id : null);

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
