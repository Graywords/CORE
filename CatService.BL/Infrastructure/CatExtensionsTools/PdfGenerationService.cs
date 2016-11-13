using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CatService.BL.Infrastructure.CatExtensionsTools.Interfaces;
using CatService.BL.Models;
using TuesPechkin;

namespace CatService.BL.Infrastructure.CatExtensionsTools
{
    public class PdfGenerationService : IPdfGenerationService
    {
        public void GeneratePdf(CatDocument catDocument)
        {
            var document = new HtmlToPdfDocument
            {
                GlobalSettings =
    {
                    
        ProduceOutline = true,
        DocumentTitle = catDocument.DocumentName,
        //PaperSize = new PechkinPaperSize() PaperKind.A4, // Implicit conversion to PechkinPaperSize
        Margins =
        {
            All = 1.375,
            Unit = Unit.Centimeters
        }
    },
                Objects = {
        new ObjectSettings { HtmlText = catDocument.DocFile.ToString() },
        new ObjectSettings { PageUrl = "" }
    }
            };
            IConverter converter = new ThreadSafeConverter(new RemotingToolset<PdfToolset>(new WinAnyCPUEmbeddedDeployment(new TempFolderDeployment())));
            var c = converter.Convert(document);

        }

        public void GeneratePdf(string url)
        {
            
        }
    }
}
