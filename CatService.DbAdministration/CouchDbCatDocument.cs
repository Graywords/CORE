using System.Net.Http;
using CatService.BL.Constants;
using CatService.BL.HttpClientWrapper.Interfaces;
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

     
    }
}
