using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CatService.BL.Constants;
using CatService.BL.HttpClientWrapper.Interfaces;
using CatService.BL.Infrastructure.CatExtensionsTools.Interfaces;
using CatService.BL.Models;

namespace CatService.BL.Infrastructure.CatExtensionsTools
{
    public class CatSupportTools : ICatSupportTools
    {
        private readonly ICatRestClient catRestClient;
        public CatSupportTools(ICatRestClient catRestClient)
        {
            this.catRestClient = catRestClient;
        }

        public string GetCouchUuid()
        {
            var u = catRestClient.MakeApiRequest<CouchUuid>(CouchDbConstants.UuidsRequest, HttpMethod.Get, null);
            return u.Identifiers[0];
        }
    }
}
