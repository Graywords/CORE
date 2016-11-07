using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatService.BL.Constants;
using CatService.DbAdministration.Models;
using Newtonsoft.Json;

namespace CatService.DbAdministration.Views
{
    class CatDocumentsView
    {
        [JsonProperty(CouchDbConstants.ByUserId)]
        public object SearchByNameView = new ViewFunction
        {
            Map = "function(doc) { emit(doc.CreatedUserId, doc) }"
        };
    }
}
