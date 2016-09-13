using System.Threading.Tasks;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.Mappings;
using CatService.Models;
using Microsoft.AspNet.Identity;

namespace CatService.Infrastructure
{
	public class CatUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>
	{
		private readonly ICatUserService catUserService;

		public CatUserStore(ICatUserService catUserService)
		{
			this.catUserService = catUserService;
		}

		public void Dispose()
		{

		}

		public Task CreateAsync(ApplicationUser user)
		{
			return Task.Factory.StartNew(() => this.catUserService.CreateCatUser(user.Map()));
		}

		public Task UpdateAsync(ApplicationUser user)
		{
		    return Task.Factory.StartNew(() => this.catUserService.UpdateCatUser(user.Map()));
        }

		public Task DeleteAsync(ApplicationUser user)
		{
			return Task.Factory.StartNew(() =>
			{
				var u = this.catUserService.FindCatUserById(user.Id);
				if (u != null)
					this.catUserService.DeleteCatUser(u);
			});
		}

		public Task<ApplicationUser> FindByIdAsync(string userId)
		{
			return Task.Factory.StartNew(() => this.catUserService.FindCatUserById(userId).Map());
		}

		public Task<ApplicationUser> FindByNameAsync(string userName)
		{
			return Task.Factory.StartNew(() => this.catUserService.FindCatUserByName(userName).Map());
		}

		public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
		{
			return Task.Factory.StartNew(() => user.PasswordHash = passwordHash);
		}

		public Task<string> GetPasswordHashAsync(ApplicationUser user)
		{
			return Task.Factory.StartNew(() => user.PasswordHash);
		}

		public Task<bool> HasPasswordAsync(ApplicationUser user)
		{
			return Task.Factory.StartNew(() => true);
		}

		public Task SetEmailAsync(ApplicationUser user, string email)
		{
			return Task.Factory.StartNew(() => true);
		}

		public Task<string> GetEmailAsync(ApplicationUser user)
		{
			return Task.Factory.StartNew(() => user.Email);
		}

		public Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
		{
			return Task.Factory.StartNew(() => true);
		}

		public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
		{
			return Task.Factory.StartNew(() => true);
		}

		public Task<ApplicationUser> FindByEmailAsync(string email)
		{
			return Task.Factory.StartNew(() => this.catUserService.FindCatUserByEmail(email).Map());
		}
	}
}