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
    public class ApiCatAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            
            ClaimsPrincipal principal = new ClaimsPrincipal(actionContext.RequestContext.Principal);
            var userId = principal.FindFirst(ClaimTypes.UserData).Value;
            

        }
        
    }
}