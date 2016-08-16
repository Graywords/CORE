using CatService.BL.Constants;
using CatService.DbAdministration.Models;
using Newtonsoft.Json;

namespace CatService.DbAdministration.Views
{
	public class CatUserViews
	{
		[JsonProperty(CouchDbConstants.ByName)]
		public object SearchByNameView = new ViewFunction
		{
			Map = "function(doc) { emit(doc.Name, doc) }"
		};
	}
}
