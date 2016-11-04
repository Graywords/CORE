using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CatService.BL.Models;

namespace CatService.BL.Infrastructure.CatExtensionsTools.Interfaces
{
    public interface ICatDocumentTools
    {
        CatDocument PostedDocumentToCatDoc(HttpPostedFileBase httpPostedFileBase);
      //  FileContentResult BytesToDocument(byte[] byteMass);
    }
}
