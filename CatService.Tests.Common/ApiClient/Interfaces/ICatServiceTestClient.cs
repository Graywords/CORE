
namespace CatService.Tests.Common.ApiClient.Interfaces
{
	public interface ICatServiceTestClient
	{
		bool Login(string usernameOrEmail, string password);
	}
}
