using System;
using System.Text;
using System.Net.Http;
using CatService.BL.Constants;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.BL.Infrastructure.CatExtensionsTools.Interfaces;
using CatService.BL.Models;

namespace CatService.BL.Infrastructure.CatExtensionsTools
{
    public class CatSupportToolsService : ICatSupportToolsService
    {
        private readonly ICatRestClient catRestClient;
		public CatSupportToolsService(ICatRestClient catRestClient)
        {
            this.catRestClient = catRestClient;
        }

        public string GetCouchUuid()
        {
            var u = catRestClient.MakeApiRequest<CouchUuid>(CouchDbConstants.UuidsRequest, HttpMethod.Get, null);
            return u.Identifiers[0];
        }

        public CatDocument GetHtml(string url)
        {
	        var siteResponse = catRestClient.MakeApiRequest(url, HttpMethod.Get, null);
	        if (!siteResponse.Success)
		        return null;

	        return new CatDocument
			{
				CreateDateTime = DateTime.UtcNow,
				DocFile = Encoding.Unicode.GetBytes(siteResponse.Raw),
				DocumentName = "temp.html",
				MimeType = "application/html"
			};
        }
    }
}
