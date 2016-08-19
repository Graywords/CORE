using System.Net.Http;
using CatService.ApiTests.Infrastructure;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Ninject;
using NSubstitute;

namespace CatService.ApiTests
{
	[TestClass]
	public class CouchDbContextServiceTest : BaseTestClass
	{
		private const string UuidJson = @"{""uuids"":[""fc1fbc8521798e03a98bb7f844006339""]}";

		[TestMethod]
		public void ShouldGetNewUuid()
		{
			var couchDbContextService = Kernel.Get<ICatUserService>();

			var uuid = couchDbContextService.GetCouchUuid();

			Assert.IsFalse(string.IsNullOrWhiteSpace(uuid));
		}

		[TestMethod]
		public void ShouldGetSubstitutedUuid()
		{
			var catRestSubstitute = Substitute.For<ICatRestClient>();
			catRestSubstitute.MakeApiRequest<CouchUuid>(Arg.Any<string>(), HttpMethod.Get, Arg.Any<object>()).Returns(JsonConvert.DeserializeObject<CouchUuid>(UuidJson));
			Kernel.Rebind<ICatRestClient>().ToConstant(catRestSubstitute);
			var couchDbContextService = Kernel.Get<ICatUserService>();

			var uuid = couchDbContextService.GetCouchUuid();

			Assert.AreEqual("fc1fbc8521798e03a98bb7f844006339", uuid);
		}
	}
}
