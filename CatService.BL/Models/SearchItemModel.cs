using Newtonsoft.Json;

namespace CatService.BL.Models
{
	public class SearchItemModel<T>
		where T : BaseModel
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("value")]
		public T Value { get; set; }
	}
}
