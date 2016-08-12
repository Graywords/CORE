using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.Enums;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.BL.Models;
using Newtonsoft.Json;

namespace CatService.BL.CouchDbProvider
{
	public class CouchDbContextService : ICouchDbContextService
	{
		private const string ConnectionString = "http://127.0.0.1:5984/";
		private readonly ICatRestClient catRestClient;

		public CouchDbContextService(ICatRestClient catRestClient)
		{
			this.catRestClient = catRestClient;
		}

		public string GetCouchUuid()
		{
			var u = catRestClient.MakeApiRequest<string>(ConnectionString + "_uuids", ApiRequestMethod.GET, null);
			//additional deserialization because of the plain text in response
			var uuids = JsonConvert.DeserializeObject<CouchUuid>(u);
			return uuids.Identifiers[0];
		}
	}
}
