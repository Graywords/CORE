using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CatService.BL.Enums;
using CatService.BL.HttpClientWrapper.Interfaces;
using Newtonsoft.Json;

namespace CatService.BL.HttpClientWrapper
{
	public class CatRestClient : ICatRestClient
	{
		public T MakeApiRequest<T>(string pathAndQuery, ApiRequestMethod method, object parms)
		{
			ApiResponse response = MakeApiRequestAsync(pathAndQuery, method, parms).Result;
			return response.Success ? response.Deserialize<T>() : default(T);
		}

		private async Task<ApiResponse> MakeApiRequestAsync(string pathAndQuery, ApiRequestMethod method, object parms, string serializedParams = null)
		{
			using (var client = new HttpClient())
			{
				ApiResponse result = CreateResponse(pathAndQuery, method, parms, serializedParams);
				Stopwatch w = Stopwatch.StartNew();
				try
				{
					HttpResponseMessage responseMessage;
					switch (method)
					{
						case ApiRequestMethod.POST:
							responseMessage = await client.PostAsync(result.RequestUri, result.GetContent(), CancellationToken.None);
							break;
						case ApiRequestMethod.PUT:
							responseMessage = await client.PutAsync(result.RequestUri, result.GetContent(), CancellationToken.None);
							break;
						//case ApiRequestMethod.PATCH:
						//	responseMessage = await client.PatchAsync(result.RequestUri, result.GetContent(), CancellationToken.None);
						//	break;
						case ApiRequestMethod.DELETE:
							responseMessage = await client.DeleteAsync(result.RequestUri, CancellationToken.None);
							break;
						default:
							responseMessage = await client.GetAsync(result.RequestUri, CancellationToken.None);
							break;
					}
					await result.SetResponseAsync(responseMessage);
				}
				catch (Exception ex)
				{
					result.WebError = ex;
				}
				w.Stop();
				return result;
			}
		}

		private ApiResponse CreateResponse(string pathAndQuery, ApiRequestMethod method, object parms, string serializedParams = null)
		{
			string content = null;
			HttpContent httpContent = null;
			if (serializedParams != null)
			{
				content = serializedParams;
			}
			else if (parms is HttpContent)
			{
				httpContent = parms as HttpContent;
			}
			else
			{
				content = SerializeData(parms);
			}

			return new ApiResponse(new Uri(pathAndQuery), method, content, httpContent);
		}

		private static string SerializeData(object data)
		{
			if (data == null)
				return null;
			return JsonConvert.SerializeObject(data);
		}
	}
}
