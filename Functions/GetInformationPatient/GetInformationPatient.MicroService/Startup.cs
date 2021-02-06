using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using TC.Connectors.RiskCovid;
using TC.Core.AuthConfig.DotNetCore;
using TC.Functions.GetInformationPatient.Business;

namespace GetInformationPatient.MicroService
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
            services.AddScoped(_ => new GetInformationPatientContext());
            //services.AddScoped(_ => new GetInformationPatientContext(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IGetInformationPatientBusiness, GetInformationPatientBusiness>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(
               provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddTransient<IRiskCovidManager>(_ => new RiskCovidManager(Configuration.GetSection("Connectors")["RiskCovidService"]));
            //services.AddTransient<IBotWspManager>(_ => new BotWspManager(Configuration.GetSection("Connectors")["BotWspService"]));
            AuthConfig.Configure(services, Configuration);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsDevPolicy", builder =>
                {
                    builder.WithOrigins("*")
                        .WithMethods("POST")
                        .AllowAnyHeader();
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsDevPolicy");
            app.UseMvc();
        }
    }
}
