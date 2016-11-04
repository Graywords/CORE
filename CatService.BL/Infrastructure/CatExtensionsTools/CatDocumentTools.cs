using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CatService.BL.Infrastructure.CatExtensionsTools.Interfaces;
using CatService.BL.Models;

namespace CatService.BL.Infrastructure.CatExtensionsTools
{
    class CatDocumentTools : ICatDocumentTools
    {
        public CatDocument PostedDocumentToCatDoc(HttpPostedFileBase httpPostedFileBase)
        {
            CatDocument result = new CatDocument();
            result.MimeType = httpPostedFileBase.ContentType;
            result.DocumentName = httpPostedFileBase.FileName;
            using (var binaryReader = new BinaryReader(httpPostedFileBase.InputStream))
            {
                result.DocFile = binaryReader.ReadBytes(httpPostedFileBase.ContentLength);
            }
            return result;
        }
       
    }
}
