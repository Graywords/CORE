using CatService.BL.Enums;

namespace CatService.BL.HttpClientWrapper.Interfaces
{
	public interface ICatRestClient
	{
		T MakeApiRequest<T>(string pathAndQuery, ApiRequestMethod method, object parms);
	}
}
