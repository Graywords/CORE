using System;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.Models;
using CatService.Tests.Common.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace CatService.BL.Tests.CatDocumentTest
{
    [TestClass]
    public class CatDocumentTest : BaseTestClass
    {
        private readonly CatDocument _catDocument = new CatDocument()
        {
            Id = "IDDQD,IDFA",
            DocumentName = "TestCatDoc",
            MimeType = "application/pdf",
            DocFile = new byte[1024],
            CreatedUserId = "ByUnitTesting"

        };

        [TestMethod]
        public void ShouldSaveDocument()
        {
            var couchDbContextService = Kernel.Get<ICatDocumentService>();
            couchDbContextService.SaveNewDocument(_catDocument);

            var doc = couchDbContextService.FindDocumentById(_catDocument.Id);
           
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Revision);
            Assert.AreEqual(doc.Id, _catDocument.Id);
            Assert.AreEqual(doc.DocumentName, _catDocument.DocumentName);
            Assert.AreEqual(doc.MimeType, _catDocument.MimeType);
            Assert.AreEqual(doc.CreatedUserId, _catDocument.CreatedUserId);
            Assert.AreEqual(DateTime.UtcNow.Date, doc.CreateDateTime.Date);
            couchDbContextService.DeleteCatDocument(doc);
            doc = couchDbContextService.FindDocumentById(_catDocument.Id);
            Assert.IsNull(doc);
        }
        [TestMethod]
        public void ShouldGetDocsList()
        {
            var couchDbContextService = Kernel.Get<ICatDocumentService>();
            Assert.IsNull(couchDbContextService.FindCatDocumentsByUserId(_catDocument.CreatedUserId));
            couchDbContextService.SaveNewDocument(_catDocument);
            couchDbContextService.SaveNewDocument(new CatDocument() {CreatedUserId = _catDocument.CreatedUserId});

            var doc = couchDbContextService.FindCatDocumentsByUserId(_catDocument.CreatedUserId);
            Assert.IsNotNull(doc);
            Assert.AreEqual(2,doc.Length);
            Assert.AreEqual(DateTime.UtcNow.Date, doc[0].CreateDateTime.Date);
            Assert.AreEqual(DateTime.UtcNow.Date, doc[1].CreateDateTime.Date);
        }
        [TestCleanup]
        public override void CleanUp()
        {
            var couchDbContextService = Kernel.Get<ICatDocumentService>();
            var docs = couchDbContextService.FindCatDocumentsByUserId(_catDocument.CreatedUserId);
            if (docs != null)
                foreach (var doc in docs)
                {
                    couchDbContextService.DeleteCatDocument(doc);
                }
            
            base.CleanUp();
        }

    }
}
