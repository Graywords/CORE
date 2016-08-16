using System;
using System.Threading.Tasks;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.Mappings;
using CatService.Models;
using Microsoft.AspNet.Identity;

namespace CatService.Infrastructure
{
	public class CatUserStore : IUserStore<ApplicationUser>
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
			return new Task(() => couchDbContextService.CreateCatUser(user.Map()));
		}

		public Task UpdateAsync(ApplicationUser user)
		{
			return new Task(() => couchDbContextService.UpdateCatUser(user.Map()));
		}

		public Task DeleteAsync(ApplicationUser user)
		{
			return new Task(() =>
			{
				var u = couchDbContextService.FindCatUserById(user.Id);
				if(u != null)
					couchDbContextService.DeleteCatUser(u);
			});
		}

		public Task<ApplicationUser> FindByIdAsync(string userId)
		{
			return new Task<ApplicationUser>(() => couchDbContextService.FindCatUserById(userId).Map());
		}

		public Task<ApplicationUser> FindByNameAsync(string userName)
		{
			return new Task<ApplicationUser>(() => this.couchDbContextService.FindCatUserByName(userName).Map());
		}
	}
}