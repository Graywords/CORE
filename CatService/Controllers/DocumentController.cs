using System.Web.Http;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.Models;
using CatService.Infrastructure.Interfaces;
using CatService.Mappings;
using MultipartDataMediaFormatter.Infrastructure;


namespace CatService.Controllers
{
    [RoutePrefix("api/Document")]
    public class DocumentController : ApiController
    {
        private readonly ICatDocumentService _catDocumentService;
        private readonly ICurrentUserInformationService _userInformationService;

        public DocumentController(ICatDocumentService catDocumentService, ICurrentUserInformationService currentUserInformationService)
        {
            this._catDocumentService = catDocumentService;
            this._userInformationService = currentUserInformationService;
        }

        [Authorize]
        public IHttpActionResult AddDocument(FormData f)
        {
            HttpFile postedFile = f.Files[0].Value;
	        CatDocument catDocument = postedFile.Map(_userInformationService.GetUserId());
            _catDocumentService.SaveNewDocument(catDocument);
            return Ok();
        }
    }
}