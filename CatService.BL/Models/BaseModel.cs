using Newtonsoft.Json;

namespace CatService.BL.Models
{
	public class BaseModel
	{
		[JsonProperty("_id")]
		public string Id { get; set; }

		public string Revision { get; set; }
	}
}
