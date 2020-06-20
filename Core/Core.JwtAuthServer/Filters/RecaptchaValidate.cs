using TC.Core.JwtAuthServer.Entities;
using TC.Core.JwtAuthServer.Messages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TC.Core.JwtAuthServer.Filters
{
    public class RecaptchaValidate : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var isRecaptcha = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["isRecaptcha"]) && Convert.ToBoolean(ConfigurationManager.AppSettings["isRecaptcha"]);
            if (isRecaptcha)
            {
                AuthMessages messages = new AuthMessages();
                //RecaptchaManagerBCP recaptcha = new RecaptchaManagerBCP(ConfigurationManager.AppSettings["apiRecaptcha"].ToString());
                string token = string.Empty;
                var date = filterContext.ActionArguments["dto"];
                foreach (PropertyInfo propertyInfo in date.GetType().GetProperties())
                {
                    if (propertyInfo.Name == "CaptchaValue")
                    {
                        token = propertyInfo.GetValue(date, null).ToString();
                    }
                }
                //var res = recaptcha.VerifyToken(new VerifyTokenRequest()
                //{
                //    Token = token
                //});
                //if (!res.Header.IsOk)
                //{
                //    filterContext.Response = filterContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, messages.Recaptcha);
                //    return;
                //}
                //if (!res.Body.IsOK || !res.Body.IsVerified)
                //{
                //    filterContext.Response = filterContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, messages.Recaptcha);
                //    return;
                //}
            }
        }
    }
}