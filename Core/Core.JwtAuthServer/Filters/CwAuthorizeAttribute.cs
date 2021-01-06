using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TC.Core.JwtAuthServer.Filters
{
    public class CwAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var identity = (ClaimsIdentity)actionContext.ControllerContext.RequestContext.Principal.Identity;
            //var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;            
            base.OnActionExecuting(actionContext);
        }
    }
}