
namespace CatService.BL.HttpClientWrapper.Interfaces
{
	public interface ITokenAuth
	{
		void AddToken(string token);
		void RemoveToken();
	}
}
