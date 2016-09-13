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
using CatService.Infrastructure;
using Ninject;
using CatService.Infrastructure.Interfaces;



namespace CatService.Filters
{
    public class ApiCatAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        private IKernel kernel;
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            ClaimsPrincipal principal = new ClaimsPrincipal(actionContext.RequestContext.Principal);
            var userId = principal.FindFirst(ClaimTypes.UserData).Value;
            AddBindings();
            var c = kernel.Get<ICurrentUserInformationServiceSet>();
            c.SetUserId(userId);


        }

        private void AddBindings()
        {
            kernel = new StandardKernel();
            this.kernel.Bind<ICurrentUserInformationServiceSet>().To<CurrentUserInformationServiceSet>();
        }
        
    }
}