using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CatService.BL.Models
{
    class CatDocument : BaseModel
    {
        public string DocumentName { get; set; }

        public HttpPostedFile DocFile { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
