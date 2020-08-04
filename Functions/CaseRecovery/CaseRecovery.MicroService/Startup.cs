using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using CaseRecovery.Business;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TC.Connectors.TwilioRooms;
using TC.Core.AuthConfig.DotNetCore;

namespace CaseRecovery.MicroService
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
            services.AddScoped(_ => new CaseRecoveryContext(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<ICaseRecoveryBusiness, CaseRecoveryBusiness>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(
               provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            AuthConfig.Configure(services, Configuration);
            services.AddTransient<ITwilioRoomsManager>(_ => new TwilioRoomsManager(Configuration.GetSection("CreateSala").Value));
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
