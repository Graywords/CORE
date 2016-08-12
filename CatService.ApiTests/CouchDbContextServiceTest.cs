using CatService.ApiTests.Infrastructure;
using CatService.BL.CouchDbProvider.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace CatService.ApiTests
{
	[TestClass]
	public class CouchDbContextServiceTest : BaseTestClass
	{
		[TestMethod]
		public void ShouldGetNewUuid()
		{
			var couchDbContextService = Kernel.Get<ICouchDbContextService>();

			var uuid = couchDbContextService.GetCouchUuid();

			Assert.IsFalse(string.IsNullOrWhiteSpace(uuid));
		}
	}
}
