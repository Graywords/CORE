using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CatService.BL.Models
{
    public class CatDocument : BaseModel
    {
        public string DocumentName { get; set; }

        public byte[] DocFile { get; set; }

        public string MimeType { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string CreatedUserId { get; set; }

    }
}
