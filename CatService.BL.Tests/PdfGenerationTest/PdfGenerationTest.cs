using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatService.BL.Infrastructure.CatExtensionsTools.Interfaces;
using CatService.BL.Models;
using CatService.Tests.Common.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace CatService.BL.Tests.PdfGenerationTest
{
    [TestClass]
    public class PdfGenerationTest : BaseTestClass
    {
        private CatDocument _catDocument = new CatDocument()
        {
            Id = "sdfsdgdsgdfgdgdfgdfgdfggd",
            DocumentName = "testdoc",
            MimeType = "application/pdf",
            DocFile = Encoding.Default.GetBytes("  <html> < head > 111 </ head > < body > Простейший документ </ body >  </ html > ")
        };
        [TestMethod]
        public void GeneratePdfTest()
        {
            var pdfService = Kernel.Get<IPdfGenerationService>();
            var result = pdfService.GeneratePdf(_catDocument);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.DocFile);
        }
    }
}
