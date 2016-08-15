using System.Net.Http;
using CatService.ApiTests.Infrastructure;
using CatService.BL.Constants;
using CatService.BL.HttpClientWrapper.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace CatService.ApiTests
{
	[TestClass]
	public class DbAdministration : BaseTestClass
	{
		[Ignore]
		[TestMethod]
		public void CreateCatUserDatabase()
		{
			var c = Kernel.Get<ICatRestClient>();

			var apiResult = c.MakeApiRequest(CouchDbConstants.CatUsersDbRequest, HttpMethod.Put, null);

			Assert.IsTrue(apiResult.Success);
		}
	}
}
