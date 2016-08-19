using System.Net.Http;
using CatService.BL.Constants;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.BL.Models;
using CatService.DbAdministration.Models;
using CatService.DbAdministration.Views;
using CatService.Tests.Common.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace CatService.DbAdministration
{
	[TestClass]
	public class CouchDbCatUser : BaseTestClass
	{
		[Ignore]
		[TestMethod]
		public void CreateCatUserDatabase()
		{
			var c = Kernel.Get<ICatRestClient>();

			var apiResult = c.MakeApiRequest(CouchDbConstants.CatUsersDbRequest, HttpMethod.Put, null);

			Assert.IsTrue(apiResult.Success);
		}

		[Ignore]
		[TestMethod]
		public void CreateCatUserView()
		{
			var c = Kernel.Get<ICatRestClient>();

			var catUserViews = new CouchView();
			catUserViews.Views = new CatUserViews();
			catUserViews.Id = CouchDbConstants.Design + CouchDbConstants.CatUsersDb;

			var apiResult = c.MakeApiRequest(CouchDbConstants.CatUsersDbRequest, HttpMethod.Post, catUserViews);

			Assert.IsTrue(apiResult.Success);
		}

		[Ignore]
		[TestMethod]
		public void DeleteCatUserView()
		{
			var c = Kernel.Get<ICatRestClient>();

			var view = c.MakeApiRequest<BaseModel>(CouchDbConstants.CatUsersDbRequest + CouchDbConstants.Design + CouchDbConstants.CatUsersDb, HttpMethod.Get, null);

			var apiResult = c.MakeApiRequest(CouchDbConstants.CatUsersDbRequest + CouchDbConstants.Design + CouchDbConstants.CatUsersDb, HttpMethod.Delete, null, view.Revision);

			Assert.IsTrue(apiResult.Success);
		}
	}
}
