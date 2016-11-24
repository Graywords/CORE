using System.Collections.Generic;
using System.Linq;
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
	[Authorize]
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

		[HttpPost]
		[Route("AddDocument")]
		public IHttpActionResult AddDocument(FormData f)
		{
			HttpFile postedFile = f.Files[0].Value;
			CatDocument catDocument = postedFile.Map(_userInformationService.GetUserId());
			_catDocumentService.SaveNewDocument(catDocument);
			return Ok();
		}

		[HttpDelete]
		[Route("DeleteDocument")]
		public IHttpActionResult DeleteDocument(string id)
		{
			CatDocument catDocument = _catDocumentService.FindDocumentById(id);
			_catDocumentService.DeleteCatDocument(catDocument);
			return Ok();
		}

		/*public CatDocumentViewModel GetDocument(string Id)
		{
			return _catDocumentService.FindDocumentById(Id).Map();
		}*/

		[HttpGet]
		[Route("GetPdfFile")]
		[AllowAnonymous]
		public IHttpActionResult GetPdfFile([FromUri]string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest();
			var converted = _pdfGenerationService.GeneratePdf(_catDocumentService.FindDocumentById(id));
			return new FileResult(converted.DocFile, converted.MimeType, "temp.pdf");
		}

		[HttpPost]
		[Route("SaveHtml")]
		public IHttpActionResult SaveHtmlFile([FromBody]string url)
		{
			var doc = _catSupportToolsService.GetHtml(url);
			doc.CreatedUserId = _userInformationService.GetUserId();
			_catDocumentService.SaveNewDocument(doc);
			return Ok();
		}


		/*public IHttpActionResult SavePdfFile(string url)
		{
			_pdfGenerationService.GeneratePdf(url);
			return Ok();
		}*/

		[HttpGet]
		public IHttpActionResult GetDocumentsByUser()
		{
			return Ok(_catDocumentService.FindCatDocumentsByUserId(_userInformationService.GetUserId()).Select(x => x.Map()).ToArray());
		}

	}	
}