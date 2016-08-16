using Newtonsoft.Json;

namespace CatService.DbAdministration.Models
{
	public class CouchView
	{
		[JsonProperty("_id")]
		public string Id { get; set; }

		//[JsonProperty("_rev")]
		//public string Revision { get; set; }

		[JsonProperty("language")]
		public string Language = "javascript";

		[JsonProperty("views")]
		public object Views { get; set; }
	}
}
