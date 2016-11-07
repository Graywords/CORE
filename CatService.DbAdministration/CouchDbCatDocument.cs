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
	public class CouchDbCatDocument : BaseTestClass
	{

		[Ignore]
		[TestMethod]
		public void CreateCatDocumentDatabase()
		{
			var c = Kernel.Get<ICatRestClient>();

			var apiResult = c.MakeApiRequest(CouchDbConstants.CatDocumentDbRequest, HttpMethod.Put, null);

            Assert.IsTrue(apiResult.Success);
        }

        [Ignore]
        [TestMethod]
        public void CreateCatDocumentView()
        {
            var c = Kernel.Get<ICatRestClient>();

            var catDocViews = new CouchView();
            catDocViews.Views = new CatDocumentsView();
            catDocViews.Id = CouchDbConstants.Design + CouchDbConstants.CatDocumentsDb;

            var apiResult = c.MakeApiRequest(CouchDbConstants.CatDocumentDbRequest, HttpMethod.Post, catDocViews);

            Assert.IsTrue(apiResult.Success);
        }

        [Ignore]
        [TestMethod]
        public void DeleteCatDocumentView()
        {
            var c = Kernel.Get<ICatRestClient>();

            var view = c.MakeApiRequest<BaseModel>(CouchDbConstants.CatDocumentDbRequest + CouchDbConstants.Design + CouchDbConstants.CatDocumentsDb, HttpMethod.Get, null);

            var apiResult = c.MakeApiRequest(CouchDbConstants.CatDocumentDbRequest + CouchDbConstants.Design + CouchDbConstants.CatDocumentsDb, HttpMethod.Delete, null, view.Revision);

            Assert.IsTrue(apiResult.Success);
        }
    }
}