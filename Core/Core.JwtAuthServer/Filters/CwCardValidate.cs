using TC.Core.JwtAuthServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TC.Core.JwtAuthServer.Filters
{
    public class CwCardValidate : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var context = new AuthContext();
            string card = string.Empty;
            var date = filterContext.ActionArguments["dto"];
            foreach (PropertyInfo propertyInfo in date.GetType().GetProperties())
            {
                if (propertyInfo.Name == "Card")
                {
                    card = propertyInfo.GetValue(date, null).ToString();
                }
            }
            if(card == null)
            {
                filterContext.Response = filterContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, "card_pin_error");
                return;
            }
            var user = context.Users.Where(x => x.UserName == card).FirstOrDefault();
            if (user == null)
            {
                filterContext.Response = filterContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, "card_pin_error");
            }
        }
    }
}