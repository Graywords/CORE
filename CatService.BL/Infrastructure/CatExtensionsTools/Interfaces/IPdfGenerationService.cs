using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CatService.BL.Models;

namespace CatService.BL.Infrastructure.CatExtensionsTools.Interfaces
{
     public interface IPdfGenerationService
     {
         void GeneratePdf(CatDocument catDocument);
         void GeneratePdf(string url);

     }
}
