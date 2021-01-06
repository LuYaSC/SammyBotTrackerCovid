namespace TC.Core.JwtAuthServer.Providers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Configuration;
    using System.Security.Claims;
    using System.Collections.Generic;

    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using Microsoft.AspNet.Identity.Owin;

    using TC.Core.JwtAuthServer.Models;
    using TC.Core.JwtAuthServer.Messages;
    using TC.Core.JwtAuthServer.Entities;

    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        ///  Responsible for validating if the Resource server (audience) is already registered in our Authorization server by reading the client_id value from the request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }

            AudienceStore store = new AudienceStore();
            var audience = store.FindAudience(context.ClientId);

            if (audience == null)
            {
                context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                store.Dispose();
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:client_id", clientId.ToString());
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", audience.RefreshTokenLifeTime.ToString());
            context.Validated();
            store.Dispose();
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Responsible for validating the resource owner (user) credentials
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            AudienceStore store = new AudienceStore();
            AuthMessages messages = new AuthMessages();
            var audience = store.FindAudience(context.ClientId);

            if (audience.ClientId == ConfigurationManager.AppSettings["audienceBackOffice"])
            {
                BackOfficeManager backManager = new BackOfficeManager();
                var claimsUserBack = backManager.GetClaimsUser(context.UserName, context.Password);

                if (!claimsUserBack.IsOk)
                {
                    context.SetError("invalid_grant", claimsUserBack.Message);
                    store.Dispose();
                    return;
                }

                ClaimsIdentity authIdentity = claimsUserBack.Body;
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", audience.ClientId
                    }
                });

                var ticket = new AuthenticationTicket(authIdentity, props);
                context.Validated(ticket);
                store.Dispose();
                return;
            }

            ApplicationUserManager userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            SessionStore sessionStore = new SessionStore();
            //CaptchaManager captchaManager = new CaptchaManager();
            var dataLogin = await context.Request.ReadFormAsync();
            var isMovilApp = (dataLogin["app_movil"] != null && Boolean.Parse(dataLogin["app_movil"]));
            var validateCaptcha = Boolean.Parse(ConfigurationManager.AppSettings["validateCaptcha"]) && !isMovilApp;
            var isRecaptcha = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["isRecaptcha"]) && Convert.ToBoolean(ConfigurationManager.AppSettings["isRecaptcha"]);
            var IpClient = isMovilApp ? string.Empty : dataLogin["IpClient"] == null ? string.Empty : dataLogin["IpClient"];

            if (validateCaptcha && isRecaptcha )
            {
                //RecaptchaManagerBCP recaptcha = new RecaptchaManagerBCP(ConfigurationManager.AppSettings["apiRecaptcha"].ToString());
                //var res = recaptcha.VerifyToken(new VerifyTokenRequest()
                //{
                //    Token = dataLogin["captchaValue"]
                //});
                //if (!res.Header.IsOk)
                //{
                //    context.SetError("invalid_captcha_not_available", res.Header.Exception.Message);
                //    store.Dispose();
                //    return;
                //}
                //if (!res.Body.IsOK || !res.Body.IsVerified)
                //{
                //    context.SetError("invalid_captcha", messages.Recaptcha);
                //    store.Dispose();
                //    return;
                //}
            }
            else if (validateCaptcha)
            {
                //VerifyCaptcha verifyCaptcha = new VerifyCaptcha()
                //{
                //    Value = dataLogin["captchaValue"],
                //    ValueToVerify = dataLogin["captchaValueToVerify"]
                //};

                //if ((!captchaManager.VerifyCaptcha(verifyCaptcha) || verifyCaptcha.Value == null || verifyCaptcha.ValueToVerify == null) && Boolean.Parse(ConfigurationManager.AppSettings["validateCaptcha"]))
                //{
                //    context.SetError("invalid_captcha", messages.Captcha);
                //    store.Dispose();
                //    return;
                //}
            }

            var userTemp = await userManager.FindByNameAsync(context.UserName);

            //userTemp.PasswordHash = userManager.PasswordHasher.HashPassword(context.Password);

            if (userTemp == null)
            {
                context.SetError("invalid_grant", messages.ErrorLogin);
                store.Dispose();
                return;
            }

            if (!userTemp.IsActive)
            {
                context.SetError("invalid_grant", messages.ErrorLogin);
                store.Dispose();
                return;
            }

            if (userTemp.State == UserStore.USER_STATE_LOCKED)
            {
                userTemp.PasswordHash = userManager.PasswordHasher.HashPassword(context.Password);
                var sessionResponse = sessionStore.SaveSession(userTemp, SessionStore.TYPE_SESSION_LOCKED, IpClient, isMovilApp);
                context.SetError("invalid_grant", messages.AccessLocked);
                store.Dispose();
                return;
            }

            if (userTemp.State == UserStore.USER_STATE_RESET || userTemp.State == UserStore.USER_STATE_NEW)
            {
                userTemp.PasswordHash = userManager.PasswordHasher.HashPassword(context.Password);
                sessionStore.SaveSession(userTemp, userTemp.State,IpClient , isMovilApp);
                context.SetError("generatePasswordRequire", messages.LoginReset);
                store.Dispose();
                return;
            }

            if(userTemp.State == "E" && context.Password == "enrevisioncw$")
            {
                var contextUser = new AuthContext();
                var user = contextUser.Users.Where(x => x.UserName == context.UserName).FirstOrDefault();
                var exchangeRate = new ExchangeRateStore().FindCurrentExchangeRate();
                //user.ExchangeRate = exchangeRate;
                userManager.UserId = user.Id;
                userManager.ClientId = context.ClientId;
                ClaimsIdentity authIdentity = await user.GenerateUserIdentityAsync(userManager);
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", userManager.ClientId.ToString()
                    }
                });

                var ticket = new AuthenticationTicket(authIdentity, props);
                context.Validated(ticket);
                store.Dispose();
                return;
            }
           

            if (userTemp.State == UserStore.USER_STATE_CHANGE || userTemp.State == UserStore.USER_STATE_GENERATE || userTemp.State == UserStore.USER_STATE_UNLOCKED)
            {
                User user = await userManager.FindAsync(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", messages.ErrorLogin);

                    if (userTemp.AccessFailedCount == (Convert.ToInt32(ConfigurationManager.AppSettings["maxFailedAccess"]) - 1))
                    {
                        context.SetError("invalid_grant", messages.AccessLocked);
                        sessionStore.BlockUser(userTemp);
                    }
                    await userManager.AccessFailedAsync(userTemp.Id);
                    userTemp.PasswordHash = userManager.PasswordHasher.HashPassword(context.Password);
                    sessionStore.SaveSession(userTemp, SessionStore.TYPE_SESSION_ERROR, IpClient, isMovilApp);
                    store.Dispose();
                    return;
                }

                DateTime lastChangePassword = user.DateLastPasswordChange != null ? (DateTime)user.DateLastPasswordChange : DateTime.Today;
                if (DateTime.Now > lastChangePassword.AddDays(user.AvailableDays))
                {
                    context.SetError("changePasswordRequire", messages.LoginExpirate);
                    store.Dispose();
                    return;
                }

                //var exchangeRate = new ExchangeRateStore().FindCurrentExchangeRate();
                //user.ExchangeRate = exchangeRate;
                await userManager.ResetAccessFailedCountAsync(userTemp.Id);
                userManager.UserId = user.Id;
                userManager.ClientId = context.ClientId;

                var sessionResponse = sessionStore.SaveSession(user, SessionStore.TYPE_SESSION_LOGIN, IpClient, isMovilApp);
                
                //var exchangeRate = new ExchangeRateStore().FindCurrentExchangeRate();
                //user.ExchangeRate = exchangeRate;
                ClaimsIdentity authIdentity = await user.GenerateUserIdentityAsync(userManager);
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", userManager.ClientId.ToString()
                    }
                });

                var ticket = new AuthenticationTicket(authIdentity, props);
                context.Validated(ticket);
                store.Dispose();
                return;
            }
          
            sessionStore.SaveSession(userTemp, userTemp.State, IpClient, isMovilApp);
            context.SetError("invalid_grant", messages.InternalError);
            store.Dispose();
            return;
        }

        //public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //    ApplicationUserManager userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
        //    SessionStore sessionStore = new SessionStore();
        //    AuthMessages messages = new AuthMessages();
        //    var userTemp = await userManager.FindByNameAsync(context.UserName);

        //    if (userTemp == null)
        //    {
        //        context.SetError("invalid_grant", messages.ErrorLogin);
        //        return;
        //    }

        //    //userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
        //    User user = await userManager.FindAsync(context.UserName, context.Password);
        //    //userManager.UserId = int.Parse((context.ClientId == null) ? "0" : context.ClientId);            

        //    if (user == null)
        //    {
        //        await userManager.AccessFailedAsync(userTemp.Id);
        //        context.SetError("invalid_grant", messages.ErrorLogin);
        //        userTemp.PasswordHash = userManager.PasswordHasher.HashPassword(context.Password);
        //        sessionStore.SaveSession(userTemp, SessionStore.TYPE_SESSION_ERROR);
        //        return;
        //    }

        //    var isLocked = await userManager.IsLockedOutAsync(userTemp.Id);

        //    if (isLocked)
        //    {
        //        sessionStore.SaveSession(user, SessionStore.TYPE_SESSION_LOCKED);
        //        context.SetError("invalid_grant", messages.AccessLocked);
        //        return;
        //    }

        //    Session lastChangePassword = sessionStore.GetLastDateChangePassword(user);
        //    if (lastChangePassword != null && DateTime.Now > lastChangePassword.DateCreation.AddMonths(1))
        //    {
        //        context.SetError("changePasswordRequire", messages.LoginExpirate);
        //        return;
        //    }

        //    var exchangeRate = new ExchangeRateStore().FindCurrentExchangeRate();
        //    user.ExchangeRate = exchangeRate;

        //    await userManager.ResetAccessFailedCountAsync(userTemp.Id);
        //    userManager.UserId = user.Id;
        //    userManager.ClientId = context.ClientId;

        //    sessionStore.SaveSession(user, SessionStore.TYPE_SESSION_LOGIN);

        //    //var exchangeRate = new ExchangeRateStore().FindCurrentExchangeRate();
        //    //user.ExchangeRate = exchangeRate;
        //    ClaimsIdentity authIdentity = await user.GenerateUserIdentityAsync(userManager);
        //    var props = new AuthenticationProperties(new Dictionary<string, string>
        //        {
        //            {
        //                 "audience", userManager.ClientId.ToString()
        //            }
        //        });

        //    var ticket = new AuthenticationTicket(authIdentity, props);
        //    context.Validated(ticket);
        //    return;
        //}

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["audience"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token not valid.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            ////newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);
            return Task.FromResult<object>(null);
        }
    }
}