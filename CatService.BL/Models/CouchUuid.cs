using Newtonsoft.Json;

namespace CatService.BL.Models
{
	public class CouchUuid
	{
		[JsonProperty("uuids")]
		public string[] Identifiers;
	}
}
