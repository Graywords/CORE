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
        private string Id;
        public void ShouldUploadDocument()
        {
            //const string newPassword = "NewP@ssw0rd";
            var c = Kernel.Get<ICatServiceTestClient>();
            c.Login("Mega Mega1111111111", "Everlight21!");
            var result = c.AddDocument("myfile.pdf", "application/pdf", new byte[1024]);
            Assert.IsTrue(result);
        }
        public override void CleanUp()
        {
            var c = Kernel.Get<ICatServiceTestClient>();
            var c1 = Kernel.Get<ICatDocumentService>();
            c.Login("Mega Mega1111111111", "Everlight21!");
           // c1.DeleteCatDocument();
            base.CleanUp();
        }
    }
}
