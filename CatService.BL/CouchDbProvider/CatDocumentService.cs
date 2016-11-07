using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CatService.BL.Constants;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.BL.Infrastructure.CatExtensionsTools.Interfaces;
using CatService.BL.Models;


namespace CatService.BL.CouchDbProvider
{
    public class CatDocumentService : ICatDocumentService
    {
        private readonly ICatRestClient _catRestClient;
        private readonly ICatSupportTools _catSupportTools;

        public CatDocumentService(ICatRestClient catRestClient, ICatSupportTools catSupportTools)
        {
            this._catRestClient = catRestClient;
            this._catSupportTools = catSupportTools;
        }


        public void SaveDocument(CatDocument catDocument)
        {
            if (catDocument.Id == null) catDocument.Id = _catSupportTools.GetCouchUuid();
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

        public List<CatDocument> FindCatDocumentsByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return null;

            var query = string.Format(CouchDbConstants.SearchByKeyFormat, CouchDbConstants.CatDocumentsViewRequest + CouchDbConstants.ByUserId, userId);

            var results = _catRestClient.MakeApiRequest<SearchResultsModel<CatDocument>>(query, HttpMethod.Get, null);
            if (results != null && results.Results.Any())
            {
                List<CatDocument> returnResult = new List<CatDocument>(results.Results.Length);
                
                foreach (var element in results.Results) returnResult.Add(element.Value);
                
                return returnResult;
            }

            return null;
        }
    }
}
