using CatService.BL.Models;

namespace CatService.BL.CouchDbProvider.Interfaces
{
	public interface ICatUserService
	{
		//string GetCouchUuid();
		void CreateCatUser(CatUser catUser);
		void UpdateCatUser(CatUser catUser);
		CatUser FindCatUserById(string userId);
		void DeleteCatUser(CatUser catUser);
		CatUser FindCatUserByName(string userName);
		CatUser FindCatUserByEmail(string email);
	}
}
