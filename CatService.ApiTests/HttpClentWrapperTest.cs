using System.Linq;
using CatService.ApiTests.Infrastructure;
using CatService.BL.Enums;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Ninject;

namespace CatService.ApiTests
{
	[TestClass]
	public class HttpClentWrapperTest : BaseTestClass
	{
		[TestMethod]
		public void MakeApiRequestTest()
		{
			var c = Kernel.Get<ICatRestClient>();

			var apiResult = c.MakeApiRequest<string>("http://127.0.0.1:5984/_uuids", ApiRequestMethod.GET, null);

			var uuids = JsonConvert.DeserializeObject<CouchUuid>(apiResult);

			Assert.IsNotNull(apiResult);
			Assert.IsNotNull(uuids);
			Assert.IsNotNull(uuids.Identifiers.Single());
		}
	}
}
