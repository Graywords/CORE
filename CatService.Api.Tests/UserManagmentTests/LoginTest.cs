using CatService.Tests.Common.ApiClient;
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
		public void ShouldLogin()
		{
			var c = Kernel.Get<ICatServiceTestClient>();

			var loginSucceeded = c.Login("Mega Mega1111111111", "Everlight21!");

			Assert.IsTrue(loginSucceeded);
		}
	}
}
