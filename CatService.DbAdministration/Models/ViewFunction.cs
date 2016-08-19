	using Newtonsoft.Json;

namespace CatService.DbAdministration.Models
{
	public class ViewFunction
	{
		[JsonProperty("map")]
		public string Map { get; set; }
	}
}
