using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;

namespace TC.Core.JwtAuthServer.Filters
{
    public class ValidationModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}