using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace CatService.Controllers
{
    [RoutePrefix("api/Document")]
    public class DocumentController : ApiController
    {
        [Authorize]
        public IHttpActionResult AddDocument(HttpPostedFileBase postedFile)
        {
            return Ok();
        }
    }
}