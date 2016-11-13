using System.Collections.Generic;
using System.Web.Http;
using CatService.BL.CouchDbProvider.Interfaces;
using CatService.BL.Infrastructure.CatExtensionsTools.Interfaces;
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
        private readonly IPdfGenerationService _pdfGenerationService;
        private readonly ICatSupportToolsService _catSupportToolsService;
        public DocumentController(ICatDocumentService catDocumentService, ICurrentUserInformationService currentUserInformationService, IPdfGenerationService pdfGenerationService, ICatSupportToolsService catSupportToolsService)
        {
            this._catDocumentService = catDocumentService;
            this._userInformationService = currentUserInformationService;
            this._pdfGenerationService = pdfGenerationService;
            _catSupportToolsService = catSupportToolsService;
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
        public HttpFile GetPdfFile(string Id)
        {

        }

        [Authorize]
        public IHttpActionResult SaveHtmlFile(string url)
        {
            var doc = _catSupportToolsService.GetHtml(url);
            _catDocumentService.SaveNewDocument(doc);
            var pdf = _pdfGenerationService.GeneratePdf(doc);
            HttpFile file = new HttpFile() {};
            _catDocumentService.SaveNewDocument(pdf);
            return Ok();
        }

        [Authorize]
        public IHttpActionResult SavePdfFile(string url)
        {
            _pdfGenerationService.GeneratePdf(url);
            r



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