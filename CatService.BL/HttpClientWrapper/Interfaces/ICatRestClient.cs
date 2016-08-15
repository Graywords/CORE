using System.Net.Http;

namespace CatService.BL.HttpClientWrapper.Interfaces
{
	public interface ICatRestClient
	{
		T MakeApiRequest<T>(string pathAndQuery, HttpMethod method, object parms, string revision = null);
		ApiResponse MakeApiRequest(string pathAndQuery, HttpMethod method, object parms, string revision = null);
	}
}
