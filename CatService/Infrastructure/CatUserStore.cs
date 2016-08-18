using System.Threading.Tasks;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.Mappings;
using CatService.Models;
using Microsoft.AspNet.Identity;

namespace CatService.Infrastructure
{
	public class CatUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>
	{
		private readonly ICouchDbContextService couchDbContextService;

		public CatUserStore(ICouchDbContextService couchDbContextService)
		{
			this.couchDbContextService = couchDbContextService;
		}

		public void Dispose()
		{

		}

		public Task CreateAsync(ApplicationUser user)
		{
			return Task.Factory.StartNew(() => couchDbContextService.CreateCatUser(user.Map()));
		}

		public Task UpdateAsync(ApplicationUser user)
		{
			return Task.Factory.StartNew(() => couchDbContextService.UpdateCatUser(user.Map()));
		}

		public Task DeleteAsync(ApplicationUser user)
		{
			return Task.Factory.StartNew(() =>
			{
				var u = couchDbContextService.FindCatUserById(user.Id);
				if (u != null)
					couchDbContextService.DeleteCatUser(u);
			});
		}

		public Task<ApplicationUser> FindByIdAsync(string userId)
		{
			return Task.Factory.StartNew(() => couchDbContextService.FindCatUserById(userId).Map());
		}

		public Task<ApplicationUser> FindByNameAsync(string userName)
		{
			return Task.Factory.StartNew(() => couchDbContextService.FindCatUserByName(userName).Map());
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
			return Task.Factory.StartNew(() => couchDbContextService.FindCatUserByEmail(email).Map());
		}
	}
}