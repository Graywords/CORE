using System.IO;
using System.Web;
using CatService.BL.Infrastructure.CatExtensionsTools.Interfaces;
using CatService.BL.Models;

namespace CatService.BL.Infrastructure.CatExtensionsTools
{
    public class CatDocumentTools : ICatDocumentTools
    {
        public CatDocument PostedDocumentToCatDoc(HttpPostedFile httpPostedFile)
        {
            CatDocument result = new CatDocument();
            result.MimeType = httpPostedFile.ContentType;
            result.DocumentName = httpPostedFile.FileName;
            using (var binaryReader = new BinaryReader(httpPostedFile.InputStream))
            {
                result.DocFile = binaryReader.ReadBytes(httpPostedFile.ContentLength);
            }
            return result;
        }
       
    }
}
