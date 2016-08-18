
using CatService.BL.Models;

namespace CatService.BL.CouchDbProvider.Interfaces
{
	public interface ICouchDbContextService
	{
		string GetCouchUuid();
		void CreateCatUser(CatUser catUser);
		void UpdateCatUser(CatUser catUser);
		void MergeCatUser(CatUser catUser);
		CatUser FindCatUserById(string userId);
		void DeleteCatUser(CatUser catUser);
		CatUser FindCatUserByName(string userName);
		CatUser FindCatUserByEmail(string email);
	}
}
