using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CatService.BL.CouchDbProvider;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.Infrastructure.CatExtensionsTools;
using CatService.BL.Models;


namespace CatService.Controllers
{
    [RoutePrefix("api/Document")]
    public class DocumentController : ApiController
    {
        private readonly ICatDocumentService _catDocumentService;
        private readonly CatDocumentTools _catDocumentTools;

        public DocumentController(ICatDocumentService catDocumentService)
        {
            this._catDocumentTools = new CatDocumentTools();
            this._catDocumentService = catDocumentService;
        }

        [Authorize]
        public IHttpActionResult AddDocument()
        {
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
            CatDocument catDocument = new CatDocument();
            catDocument = _catDocumentTools.PostedDocumentToCatDoc(postedFile);
            _catDocumentService.SaveDocument(catDocument);

            return Ok();
        }
    }
}