using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using CatService.Providers;
using CatService.Models;

namespace CatService
{
	public partial class Startup
	{
		public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

		private void ConfigureAuth(IAppBuilder app)
		{
			app.CreatePerOwinContext(ApplicationDbContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

			// Configure the application for OAuth based flow
			OAuthOptions = new OAuthAuthorizationServerOptions
			{
				//#if DEBUG
				AllowInsecureHttp = true,
				//#endif
				TokenEndpointPath = new PathString("/token"),
				Provider = new CatOAuthAuthorizationServerProvider(),
				AccessTokenExpireTimeSpan = TimeSpan.FromHours(6),
			};

			app.UseOAuthAuthorizationServer(OAuthOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions { Provider = new OAuthBearerAuthenticationProvider() });
		}
	}

	public class CatOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			if (context.ClientId == null)
				context.Validated();

			return Task.FromResult<object>(null);
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			try
			{
				var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

				ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

				if(user == null)
					throw new InvalidOperationException("invalid username or password");

				var identity = GetTicket(user.Id, context.Options.AuthenticationType);

				context.Validated(identity);

			}
			catch (InvalidOperationException ex)
			{
				context.SetError("invalid_grant", ex.Message);
			}
		}

		public static ClaimsIdentity GetTicket(string userId, string authenticationType)
		{
			var identity = new ClaimsIdentity(authenticationType);
			identity.AddClaim(new Claim(ClaimTypes.UserData, userId.ToString(CultureInfo.InvariantCulture)));
			return identity;
		}
	}
}
