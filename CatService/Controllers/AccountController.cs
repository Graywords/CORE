using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CatService.Models;

namespace CatService.Controllers
{
	[Authorize]
	[RoutePrefix("api/Account")]
	public class AccountController : ApiController
	{
		private ApplicationUserManager userManager;

		public AccountController()
		{

		}

		public AccountController(ApplicationUserManager userManager, ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
		{
			UserManager = userManager;
			AccessTokenFormat = accessTokenFormat;
		}

		private IAuthenticationManager AuthenticationManager { get { return Request.GetOwinContext().Authentication; } }

		public ApplicationUserManager UserManager
		{
			get
			{
				return this.userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				this.userManager = value;
			}
		}

		public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

		// POST api/Account/ChangePassword
		//[Route("ChangePassword")]
		//public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
		//		model.NewPassword);

		//	if (!result.Succeeded)
		//	{
		//		return GetErrorResult(result);
		//	}

		//	return Ok();
		//}

		// POST api/Account/SetPassword
		//[Route("SetPassword")]
		//public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

		//	if (!result.Succeeded)
		//	{
		//		return GetErrorResult(result);
		//	}

		//	return Ok();
		//}

		// POST api/Account/Register
		[AllowAnonymous]
		[Route("Register")]
		public async Task<IHttpActionResult> Register(RegisterBindingModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = new ApplicationUser() { UserName = model.Name, Email = model.Email };

			IdentityResult result = await UserManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			return Ok();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.userManager != null)
			{
				this.userManager.Dispose();
				this.userManager = null;
			}

			base.Dispose(disposing);
		}

		#region Helpers

		private IHttpActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return InternalServerError();
			}

			if (!result.Succeeded)
			{
				if (result.Errors != null)
				{
					foreach (string error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}

				if (ModelState.IsValid)
				{
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return BadRequest();
				}

				return BadRequest(ModelState);
			}

			return null;
		}

		#endregion
	}
}
