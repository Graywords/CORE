using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.Tests.Common.ApiClient.Interfaces;
using CatService.Tests.Common.Infrastructure;

namespace CatService.Tests.Common.ApiClient
{
	public class CatServiceTestClient : ICatServiceTestClient
	{
		private readonly ICatRestClient _catRestClient;

		public CatServiceTestClient(ICatRestClient catRestClient)
		{
			_catRestClient = catRestClient;
		}

		public bool Login(string userName, string password)
		{
			var p = new Dictionary<string, string>
			{
				{"grant_type", "password"},
				{"username", userName},
				{"password", password}
			};
			var content = new FormUrlEncodedContent(p);
			var token = _catRestClient.MakeApiRequest<OAuthToken>(Constants.Constants.RequestToken, HttpMethod.Post, content);

			return token != null;
		}
	}
}
