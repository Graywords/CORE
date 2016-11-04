using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CatService.BL.Constants;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.BL.Models;

namespace CatService.BL.CouchDbProvider
{
    public class CatDocumentService : ICatDocumentService
    {
        private readonly ICatRestClient _catRestClient;
        public CatDocumentService(ICatRestClient catRestClient)
        {
            this._catRestClient = catRestClient;
        }


        public void SaveDocument(CatDocument catDocument)
        {
            catDocument.CreateDateTime = DateTime.UtcNow;
            var r = _catRestClient.MakeApiRequest<CouchDbResponseModel>(CouchDbConstants.CatDocumentDbRequest, HttpMethod.Post, catDocument);
            if (!r.Ok)
                throw new InvalidOperationException("Create Of Cat Document Failed");
            catDocument.Revision = r.Revision;
        }

        public CatDocument FindDocumentById(string Id)
        {
            return _catRestClient.MakeApiRequest<CatDocument>(CouchDbConstants.CatDocumentDbRequest + Id, HttpMethod.Get, null);
        }

        public void DeleteCatDocument(CatDocument catDocument)
        {
            if (catDocument == null)
                throw new NullReferenceException("catDocument not provided");

            if (string.IsNullOrWhiteSpace(catDocument.Revision))
                throw new NullReferenceException("Revision should be provided for deletion of catDocument");

            _catRestClient.MakeApiRequest(CouchDbConstants.CatDocumentDbRequest + catDocument.Id, HttpMethod.Delete, null, catDocument.Revision);
        }
    }
}
