using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CatService.BL.CouchDbProvider;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.Infrastructure.CatExtensionsTools;
using CatService.BL.Models;
using CatService.Infrastructure.Interfaces;


namespace CatService.Controllers
{
    [RoutePrefix("api/Document")]
    public class DocumentController : ApiController
    {
        private readonly ICatDocumentService _catDocumentService;
        private readonly CatDocumentTools _catDocumentTools;
        private readonly ICurrentUserInformationService _userInformationService;

        public DocumentController(ICatDocumentService catDocumentService, ICurrentUserInformationService currentUserInformationService)
        {
            this._catDocumentTools = new CatDocumentTools();
            this._catDocumentService = catDocumentService;
            this._userInformationService = currentUserInformationService;
        }

        [Authorize]
        public IHttpActionResult AddDocument()
        {
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
            CatDocument catDocument = new CatDocument();
            catDocument = _catDocumentTools.PostedDocumentToCatDoc(postedFile);
            catDocument.CreatedUserId = _userInformationService.GetUserId();
            _catDocumentService.SaveDocument(catDocument);

            return Ok();
        }
    }
}