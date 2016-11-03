using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatService.BL.Models;

namespace CatService.BL.CouchDbProvider.Interfaces
{
    interface ICatDocumentService
    {
        void SaveDocument(CatDocument catDocument);

        CatDocument FindDocumentById(string Id);

        void DeleteCatDocument(CatDocument catDocument);
    }
}
