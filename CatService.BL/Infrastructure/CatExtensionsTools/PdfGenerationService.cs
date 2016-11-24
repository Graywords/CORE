using System.Text;
using CatService.BL.Infrastructure.CatExtensionsTools.Interfaces;
using CatService.BL.Models;
using TuesPechkin;


namespace CatService.BL.Infrastructure.CatExtensionsTools
{
	public class PdfGenerationService : IPdfGenerationService
	{
		public CatDocument GeneratePdf(CatDocument catDocument)
		{
			var document = new HtmlToPdfDocument
			{
				GlobalSettings =
	{

		ProduceOutline = true,
		DocumentTitle = catDocument.DocumentName,
		//PaperSize = PaperKind.A4, // Implicit conversion to PechkinPaperSize
		//PaperSize = new PechkinPaperSize();
		Margins =
		{
			All = 1.375,
			Unit = Unit.Centimeters
		}
	},
				Objects = {
        new ObjectSettings { HtmlText = Encoding.Unicode.GetString(catDocument.DocFile), }
        //new ObjectSettings { PageUrl = "https://www.tut.by" }
    }
			};
			IConverter converter = new ThreadSafeConverter(new RemotingToolset<PdfToolset>(new WinAnyCPUEmbeddedDeployment(new TempFolderDeployment())));
			var c = converter.Convert(document);
			catDocument.DocFile = c;
			catDocument.MimeType = "application/pdf";
			return catDocument;

		}

		public void GeneratePdf(string url)
		{

		}
	}
}
