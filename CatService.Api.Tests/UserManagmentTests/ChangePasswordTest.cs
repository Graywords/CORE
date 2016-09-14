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
            var c = Kernel.Get<ICatServiceTestClient>();
            var logged = c.Login("Mega Mega1111111111", "Everlight21!");
            var changed = c.ChangePassword("Everlight21!","NewP@ssw0rd", "NewP@ssw0rd");
            Assert.IsTrue(changed);
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
