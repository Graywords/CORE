using System;
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
        private string username = "UnitTestUser1";
        private string email = "qwert@qwe.by";
        private string password = "Qpass123!";
        [TestMethod]
        public void ShouldRegister()
        {
            var c = Kernel.Get<ICatServiceTestClient>();
            var registered = c.Register(username, email, password, password);
            Assert.IsTrue(registered);
        }

        [TestCleanup]
        public override void CleanUp()
        {
            var c = Kernel.Get<ICatUserService>();
            c.DeleteCatUser(c.FindCatUserByName(username));
            base.CleanUp();
            
        }
    }
}
