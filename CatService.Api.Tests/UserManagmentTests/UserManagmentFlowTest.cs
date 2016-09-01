using System;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.Tests.Common.ApiClient.Interfaces;
using CatService.Tests.Common.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace CatService.Api.Tests.UserManagmentTests
{
    [TestClass]
    public class UserManagmentFlowTest : BaseTestClass
    {

        private string username = "UnitTestUser2";
        private string email = "qwert@qwe.by";
        private string password = "Qpass123!";
        [TestMethod]
        public void FullUserWorkingCykle()
        {
            var c = Kernel.Get<ICatServiceTestClient>();
            var registered = c.Register(username, email, password, password);
            var logged = c.Login(username, password);

            Assert.IsTrue(registered & logged);
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
