using Newtonsoft.Json;

namespace CatService.BL.Models
{
	public class SearchResultsModel<T>
		where T : BaseModel
	{
		[JsonProperty("total_rows")]
		public int TotalCount { get; set; }

		[JsonProperty("offset")]
		public int Offset { get; set; }

		[JsonProperty("rows")]
		public SearchItemModel<T>[] Results { get; set; }
	}
}
