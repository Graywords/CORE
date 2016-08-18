using System;
using System.Linq;
using System.Net.Http;
using CatService.BL.Constants;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.BL.Infrastructure;
using CatService.BL.Models;

namespace CatService.BL.CouchDbProvider
{
	public class CouchDbContextService : ICouchDbContextService
	{
		private readonly ICatRestClient catRestClient;

		public CouchDbContextService(ICatRestClient catRestClient)
		{
			this.catRestClient = catRestClient;
		}

		public string GetCouchUuid()
		{
			var u = catRestClient.MakeApiRequest<CouchUuid>(CouchDbConstants.UuidsRequest, HttpMethod.Get, null);
			return u.Identifiers[0];
		}

		public void CreateCatUser(CatUser catUser)
		{
			catUser.CreatedOn = DateTime.UtcNow;
			var r = catRestClient.MakeApiRequest<CouchDbResponseModel>(CouchDbConstants.CatUsersDbRequest, HttpMethod.Post, catUser);
			if (!r.Ok)
				throw new InvalidOperationException("Create Of Cat User Failed");
			catUser.Revision = r.Revision;
		}

		public void UpdateCatUser(CatUser catUser)
		{
			if (catUser == null)
				throw new NullReferenceException("catUser not provided");

			if (string.IsNullOrWhiteSpace(catUser.Revision))
				throw new NullReferenceException("Revision should be provided for deletion of catUser");

			var revision = catUser.ExpireRevision();

			var r = catRestClient.MakeApiRequest<CouchDbResponseModel>(CouchDbConstants.CatUsersDbRequest + catUser.Id, HttpMethod.Put, catUser, revision);
			if(!r.Ok)
				throw new InvalidOperationException("Update Of Cat User Failed");
			catUser.Revision = r.Revision;
		}

		public void MergeCatUser(CatUser catUser)
		{
			if (catUser == null)
				throw new NullReferenceException("catUser not provided");

			if (string.IsNullOrWhiteSpace(catUser.Revision))
				CreateCatUser(catUser);
			else
				MergeCatUser(catUser);
		}

		public CatUser FindCatUserById(string userId)
		{
			return catRestClient.MakeApiRequest<CatUser>(CouchDbConstants.CatUsersDbRequest + userId, HttpMethod.Get, null);
		}

		public void DeleteCatUser(CatUser catUser)
		{
			if(catUser == null)
				throw new NullReferenceException("catUser not provided");

			if(string.IsNullOrWhiteSpace(catUser.Revision))
				throw new NullReferenceException("Revision should be provided for deletion of catUser");

			catRestClient.MakeApiRequest(CouchDbConstants.CatUsersDbRequest + catUser.Id, HttpMethod.Delete, null, catUser.Revision);
		}

		public CatUser FindCatUserByName(string userName)
		{
			if (string.IsNullOrWhiteSpace(userName))
				return null;

			var query = string.Format(CouchDbConstants.SearchByKeyFormat, CouchDbConstants.CatUsersViewRequest + CouchDbConstants.ByName, userName);

			var results = catRestClient.MakeApiRequest<SearchResultsModel<CatUser>>(query, HttpMethod.Get, null);
			if (results != null && results.Results.Any())
				return results.Results[0].Value;

			return null;
		}

		public CatUser FindCatUserByEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return null;

			var query = string.Format(CouchDbConstants.SearchByKeyFormat, CouchDbConstants.CatUsersViewRequest + CouchDbConstants.ByEmail, email);

			var results = catRestClient.MakeApiRequest<SearchResultsModel<CatUser>>(query, HttpMethod.Get, null);
			if (results != null && results.Results.Any())
				return results.Results[0].Value;

			return null;
		}
	}
}
