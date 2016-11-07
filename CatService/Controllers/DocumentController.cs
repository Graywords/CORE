using System.Collections.Generic;
using System.Web.Http;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.Models;
using CatService.Infrastructure.Interfaces;
using CatService.Mappings;
using CatService.Models;
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

        [Authorize]
        public IHttpActionResult DeleteDocument(string id)
        {
            CatDocument catDocument = _catDocumentService.FindDocumentById(id);
            _catDocumentService.DeleteCatDocument(catDocument);
            return Ok();
        }

        [Authorize]
        public CatDocumentViewModel GetDocument(string Id)
        {
            return _catDocumentService.FindDocumentById(Id).Map();
        }
        [Authorize]
        public List<CatDocumentViewModel> GetDocumentsByUser(string userId)
        {
            var temp = _catDocumentService.FindCatDocumentsByUserId(userId);
            List<CatDocumentViewModel> result = new List<CatDocumentViewModel>();
            foreach (var element in temp)
            {
                result.Add(element.Map());   
            }
            return result;
        }

    }
}