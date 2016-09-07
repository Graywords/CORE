using CatService.Tests.Common.ApiClient.Interfaces;
using CatService.Tests.Common.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using CatService.BL.CouchDbProvider.Interfaces;

namespace CatService.Api.Tests.UserManagmentTests
{
    [TestClass]
    public class RegisterTest : BaseTestClass
    {
	    private const string Username = "UnitTestUser1";
	    private const string Email = "qwert123@qwe.by";
	    private const string Password = "Qpass123!";

	    [TestMethod]
        public void ShouldRegister()
        {
            var c = Kernel.Get<ICatServiceTestClient>();
            var registered = c.Register(Username, Email, Password, Password);
            Assert.IsTrue(registered);
        }

        [TestCleanup]
        public override void CleanUp()
        {
            var c = Kernel.Get<ICatUserService>();
            c.DeleteCatUser(c.FindCatUserByName(Username));
            base.CleanUp();
        }
    }
}
