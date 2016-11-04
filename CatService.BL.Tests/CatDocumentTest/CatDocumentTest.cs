using System;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.Models;
using CatService.Tests.Common.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace CatService.BL.Tests.CatDocumentTest
{
    class CatDocumentTest : BaseTestClass
    {
        private readonly CatDocument _catDocument = new CatDocument()
        {
            Id = "qwertyuiop",
            DocumentName = "TestCatDoc",
            MimeType = "application/pdf"

        };
    }
}
