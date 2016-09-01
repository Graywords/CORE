using CatService.Tests.Common.ApiClient.Interfaces;
using Ninject;
using CatService.Tests.Common.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CatService.Api.Tests.UserManagmentTests
{
	[TestClass]
	public class LoginTest : BaseTestClass
	{
		[TestMethod]
		public void ShouldLoginAndLogout()
		{
			var c = Kernel.Get<ICatServiceTestClient>();

			Assert.IsTrue(c.Login("Mega Mega1111111111", "Everlight21!"));
		}
	}
}
