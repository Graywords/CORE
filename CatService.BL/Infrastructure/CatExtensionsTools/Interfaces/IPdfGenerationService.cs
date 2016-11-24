using CatService.BL.Models;

namespace CatService.BL.Infrastructure.CatExtensionsTools.Interfaces
{
     public interface IPdfGenerationService
     {
         CatDocument GeneratePdf(CatDocument catDocument);
         void GeneratePdf(string url);

     }
}
