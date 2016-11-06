using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.BL.Models;
using Newtonsoft.Json;

namespace CatService.BL.HttpClientWrapper
{
	public class CatRestClient : ICatRestClient, ITokenAuth
	{
		private string _token;

		public T MakeApiRequest<T>(string pathAndQuery, HttpMethod method, object parms, string revision = null)
		{
			ApiResponse response = MakeApiRequestAsync(pathAndQuery, method, parms, null, revision).Result;
			return response.Success ? response.Deserialize<T>() : default(T);
		}

		public ApiResponse MakeApiRequest(string pathAndQuery, HttpMethod method, object parms, string revision = null)
		{
			var r = MakeApiRequestAsync(pathAndQuery, method, parms, null, revision).Result;
			if (!r.Success)
				throw new InvalidOperationException(r.Raw);
			return r;
		}

		private async Task<ApiResponse> MakeApiRequestAsync(string pathAndQuery, HttpMethod method, object parms, string serializedParams = null, string revision = null)
		{
			using (var client = new HttpClient())
			{
				ApiResponse result = CreateResponse(pathAndQuery, method, parms, serializedParams);
				Stopwatch w = Stopwatch.StartNew();
				try
				{
					var requestMessage = new HttpRequestMessage(method, result.RequestUri);

					requestMessage.Content = result.GetContent();

				    if (parms is MultipartFormDataContent)
				        requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
                    else
                        requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					if (!string.IsNullOrWhiteSpace(this._token))
						requestMessage.Headers.TryAddWithoutValidation("Authorization", "Bearer " + this._token);

					if (!string.IsNullOrWhiteSpace(revision))
						requestMessage.Headers.IfMatch.Add((new EntityTagHeaderValue(revision)));

					HttpResponseMessage responseMessage = client.SendAsync(requestMessage, CancellationToken.None).Result;

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

		private ApiResponse CreateResponse(string pathAndQuery, HttpMethod method, object parms, string serializedParams = null)
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

		public void AddToken(string token)
		{
			this._token = token;
		}

		public void RemoveToken()
		{
			this._token = null;
		}
	}
}
