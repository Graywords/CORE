using System.Collections.Generic;
using CatService.BL.Models;

namespace CatService.BL.CouchDbProvider.Interfaces
{
    public interface ICatDocumentService
    {
        void SaveNewDocument(CatDocument catDocument);

        CatDocument FindDocumentById(string Id);

        void DeleteCatDocument(CatDocument catDocument);

        List<CatDocument> FindCatDocumentsByUserId(string userId);


    }
}
