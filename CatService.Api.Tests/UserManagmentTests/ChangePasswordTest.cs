using CatService.Tests.Common.ApiClient.Interfaces;
using Ninject;
using CatService.Tests.Common.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CatService.Api.Tests.UserManagmentTests
{
    [TestClass]
    public class ChangePasswordTest : BaseTestClass
    {
        [TestMethod]
        public void ShouldChangePassword()
        {
	        const string newPassword = "NewP@ssw0rd";
            var c = Kernel.Get<ICatServiceTestClient>();
            c.Login("Mega Mega1111111111", "Everlight21!");
			var changed = c.ChangePassword("Everlight21!", newPassword, newPassword);
            Assert.IsTrue(changed);
			var updatedLogin = c.Login("Mega Mega1111111111", newPassword);
			Assert.IsTrue(updatedLogin);
		}

        [TestCleanup]
        public override void CleanUp()
        {
            var c = Kernel.Get<ICatServiceTestClient>();
            c.Login("Mega Mega1111111111", "NewP@ssw0rd");
            c.ChangePassword("NewP@ssw0rd", "Everlight21!", "Everlight21!");
            base.CleanUp();
        }
    }
}
