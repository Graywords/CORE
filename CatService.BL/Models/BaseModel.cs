using Newtonsoft.Json;

namespace CatService.BL.Models
{
	public class BaseModel
	{
		private const string RevisionStringFormat = "\"{0}\"";
		private string revision;

		[JsonProperty("_id")]
		public string Id { get; set; }

		[JsonProperty("_rev", NullValueHandling = NullValueHandling.Ignore)]
		public string Revision
		{
			get { return this.revision; }
			set
			{
				this.revision = string.IsNullOrWhiteSpace(value) ? null : string.Format(RevisionStringFormat, value);
			}
		}

		public string ExpireRevision()
		{
			var r = Revision;
			Revision = null;
			return r;
		}
	}
}
