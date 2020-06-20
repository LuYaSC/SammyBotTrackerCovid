using System;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using TC.Core.JwtAuthServer.Providers;
using Microsoft.Owin.Security.OAuth;
using System.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.DataHandler.Encoder;
using TC.Core.JwtAuthServer.App_Start;
using TC.Core.JwtAuthServer.Entities;
using TC.Core.JwtAuthServer.Models;

[assembly: OwinStartup(typeof(TC.Core.JwtAuthServer.Startup))]

namespace TC.Core.JwtAuthServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);            

            // Web API routes
            //config.MapHttpAttributeRoutes();

            this.ConfigureOAuth(app);

            app.UseWebApi(config);
        }


        public void ConfigureOAuth(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.CreatePerOwinContext(AuthContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            // TODO Sacar insecure, ver url, ver endpointpath
            int minutesSession = Convert.ToInt32(ConfigurationManager.AppSettings["timeMinutesSession"]); 
            OAuthAuthorizationServerOptions authServerOptions = new OAuthAuthorizationServerOptions()
            {
                // For Dev enviroment only (on production should be AllowInsecureHttp = false)
                //#if DEBUG
                //#endif
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(minutesSession),
                Provider = new CustomOAuthProvider(),
                RefreshTokenProvider = new RefreshTokenProvider(),
                AccessTokenFormat = new Formats.CustomJwtFormat(ConfigurationManager.AppSettings["issuer"])
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(authServerOptions);

            // Configurador para consumir servicios
           var issuer = ConfigurationManager.AppSettings["issuer"];

            AudienceStore store = new AudienceStore();
            var audiences = store.ListAudiences();

            //TODO Multiples audiencias sin reiniciar
            //Api controllers with an[Authorize] attribute will be validated with
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = audiences.Select(a => a.ClientId),
                    IssuerSecurityTokenProviders =
                    audiences.Select(a => new SymmetricKeyIssuerSecurityTokenProvider(issuer, TextEncodings.Base64Url.Decode(a.Base64Secret)))
                });
            store.Dispose();
        }
    }
}
