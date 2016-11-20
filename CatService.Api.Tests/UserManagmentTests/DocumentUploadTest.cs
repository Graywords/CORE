using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.Tests.Common.ApiClient.Interfaces;
using CatService.Tests.Common.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace CatService.Api.Tests.UserManagmentTests
{
    [TestClass]
    public class DocumentUploadTest: BaseTestClass
    {
        [TestMethod]
        public void ShouldUploadDocument()
        {
            var c = Kernel.Get<ICatServiceTestClient>();
            c.Login("Mega Mega1111111111", "Everlight21!");
            var result = c.AddDocument("EmptyFileForUserManagementTesting.pdf", "application/pdf", new byte[1024]);
            Assert.IsTrue(result);
        }

		[TestMethod]
		public void ShouldSaveHtmlFile()
		{
			var c = Kernel.Get<ICatServiceTestClient>();
			c.Login("Mega Mega1111111111", "Everlight21!");
			var result = c.AddDocument("EmptyFileForUserManagementTesting.pdf", "application/pdf", new byte[1024]);
			Assert.IsTrue(result);
		}

        [TestCleanup]
        public override void CleanUp()
        {
            var c = Kernel.Get<ICatServiceTestClient>();
            var c1 = Kernel.Get<ICatDocumentService>();
            var user = Kernel.Get<ICatUserService>();
            c.Login("Mega Mega1111111111", "Everlight21!");
            c1.DeleteCatDocument(c1.FindCatDocumentsByUserId(user.FindCatUserByName("Mega Mega1111111111").Id).First());
            base.CleanUp();
        }
    }
}
