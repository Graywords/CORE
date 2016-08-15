
using CatService.BL.Models;

namespace CatService.BL.CouchDbProvider.Interfaces
{
	public interface ICouchDbContextService
	{
		string GetCouchUuid();
		void CreateCatUser(CatUser catUser);
		void UpdateCatUser(CatUser catUser);
		CatUser FindCatUserById(string userId);
		void DeleteCatUser(CatUser catUser);
	}
}
