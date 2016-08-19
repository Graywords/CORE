using System.Linq;
using System.Net.Http;
using CatService.BL.Constants;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.BL.Models;
using CatService.Tests.Common.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

			var apiResult = c.MakeApiRequest<CouchUuid>(CouchDbConstants.UuidsRequest, HttpMethod.Get, null);

			Assert.IsNotNull(apiResult);
			Assert.IsNotNull(apiResult.Identifiers.Single());
		}
	}
}
