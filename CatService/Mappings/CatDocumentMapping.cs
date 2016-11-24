using CatService.BL.Models;
using CatService.Models;
using MultipartDataMediaFormatter.Infrastructure;

namespace CatService.Mappings
{
	public static class CatDocumentMapping
	{
		public static CatDocument Map(this HttpFile httpFile, string createdUserId = null)
		{
			CatDocument result = new CatDocument();
			result.MimeType = httpFile.MediaType;
			result.DocumentName = httpFile.FileName;
			result.DocFile = httpFile.Buffer;
			result.CreatedUserId = createdUserId;
			return result;
		}

		public static CatDocumentViewModel Map(this CatDocument catDocument)
		{
			CatDocumentViewModel catDocumentViewModel = new CatDocumentViewModel();

			catDocumentViewModel.Id = catDocument.Id;
			catDocumentViewModel.Name = catDocument.DocumentName;

			return catDocumentViewModel;
		}
	}
}