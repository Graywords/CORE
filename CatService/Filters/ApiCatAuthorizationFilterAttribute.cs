using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using CatService.Infrastructure.Interfaces;

namespace CatService.Filters
{
	public class ApiCatAuthorizationFilterAttribute : AuthorizationFilterAttribute
	{
		public override void OnAuthorization(HttpActionContext actionContext)
		{
			base.OnAuthorization(actionContext);
			var principal = new ClaimsPrincipal(actionContext.RequestContext.Principal);
			var userId = principal.FindFirst(ClaimTypes.UserData).Value;
			var c = DependencyResolver.Current.GetService<ICurrentUserInformationService>();
			var currentUserInformationServiceSet = c as ICurrentUserInformationServiceSet;
			if (currentUserInformationServiceSet != null)
				currentUserInformationServiceSet.SetUserId(userId);
		}
	}
}