using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.Tests.Common.ApiClient.Interfaces;
using CatService.Tests.Common.Infrastructure;

namespace CatService.Tests.Common.ApiClient
{
	public class CatServiceTestClient : ICatServiceTestClient
	{
		private readonly ICatRestClient _catRestClient;
		private OAuthToken _oAuthToken;

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

			_oAuthToken = _catRestClient.MakeApiRequest<OAuthToken>(Constants.Constants.RequestToken, HttpMethod.Post, content);

			var authTokenRestClient = _catRestClient as ITokenAuth;
			if (authTokenRestClient != null)
				authTokenRestClient.AddToken(_oAuthToken.access_token);

			return _oAuthToken != null;
		}

		public bool Register(string userName, string email, string password, string passwordConfirm)
		{
			var p = new Dictionary<string, string>
            {
                {"name", userName},
                {"email", email},
                {"password", password},
                {"ConfirmPassword", passwordConfirm }
            };
			var content = new FormUrlEncodedContent(p);
			var reg = _catRestClient.MakeApiRequest(Constants.Constants.RequestRegister, HttpMethod.Post, content);

			return reg != null && reg.Success;
		}

	    public bool ChangePassword(string oldPassword, string newPassword, string confirmPassword)
	    {
            var p = new Dictionary<string, string>
            {
                {"OldPassword", oldPassword},
                {"NewPassword", newPassword},
                {"ConfirmPassword", confirmPassword }
            };
            var content = new FormUrlEncodedContent(p);
            var reg = _catRestClient.MakeApiRequest(Constants.Constants.RequestChangePassword, HttpMethod.Post, content);

            return reg != null && reg.Success;
        }

	    
	    public bool AddDocument(string filename, string mimeType, byte[] fileData)
	    {
            var content = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(fileData);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(mimeType);
            content.Add(fileContent,"document", filename);
            var res = _catRestClient.MakeApiRequest(Constants.Constants.RequestAddDocument, HttpMethod.Post, content);
            return res != null && res.Success;
        }

		public bool SavePageDocument(string url)
		{
			var res = _catRestClient.MakeApiRequest(Constants.Constants.RequestSavePageDocument, HttpMethod.Post, url);
			return res != null && res.Success;
		}
	}
}
