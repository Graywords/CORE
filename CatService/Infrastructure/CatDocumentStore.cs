using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CatService.BL.CouchDbProvider;
using CatService.BL.CouchDbProvider.Interfaces;

namespace CatService.Infrastructure
{
    public class CatDocumentStore
    {
        public readonly ICatDocumentService catDocumentService;

        public CatDocumentStore(ICatDocumentService catDocumentService)
        {
            this.catDocumentService = catDocumentService;
        }
        public void Dispose()
        {

        }

    }
}