
namespace CatService.Tests.Common.ApiClient.Interfaces
{
	public interface ICatServiceTestClient
	{
		bool Login(string usernameOrEmail, string password);
	    bool Register(string userName, string email, string password, string passwordConfirm);
	}
}
