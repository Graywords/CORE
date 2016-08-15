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
			throw new NotImplementedException();
		}

		public Task DeleteAsync(ApplicationUser user)
		{
			throw new NotImplementedException();
		}

		public Task<ApplicationUser> FindByIdAsync(string userId)
		{
			return new Task<ApplicationUser>(() => couchDbContextService.FindCatUserById(userId).Map());
		}

		public Task<ApplicationUser> FindByNameAsync(string userName)
		{
			throw new NotImplementedException();
		}
	}
}