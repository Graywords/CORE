using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using IAuthorizationFilter = System.Web.Mvc.IAuthorizationFilter;

namespace CatService.Filters
{
    public class CatAuthorizationFilterAttribute : AuthorizationFilterAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var a = HttpContext.Current.User.Identity.GetUserId();
            base.OnAuthorization(actionContext);
       
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var a = HttpContext.Current.User.Identity.GetUserId();
            //base.OnAuthorization(filterContext);
        }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {

            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (!principal.Identity.IsAuthenticated)
            {
                return Task.FromResult<object>(null);
            }

            var userName = principal.FindFirst(ClaimTypes.Name).Value;
            var userAllowedTime = principal.FindFirst("userAllowedTime").Value;


            //User is Authorized, complete execution
            return Task.FromResult<object>(null);

        }
    }
}