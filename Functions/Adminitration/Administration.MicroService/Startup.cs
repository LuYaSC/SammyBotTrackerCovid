using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TC.Connectors.BotWsp;
using TC.Connectors.JwtAuth;
using TC.Core.AuthConfig.DotNetCore;
using TC.Functions.Administration.Business;

namespace TC.Functions.Administration.MicroService
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
            services.AddScoped(_ => new AdministrationContext(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IAdministrationBusiness, AdministrationBusiness>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(
               provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddTransient<IJwtAuthManager>(_ => new JwtAuthManager(Configuration.GetSection("Connectors")["JwtAuthService"]));
            services.AddTransient<IBotWspManager>(_ => new BotWspManager(Configuration.GetSection("Connectors")["BotWspService"]));
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
            else
            {
                app.UseHsts();
            }
            app.UseCors("CorsDevPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
