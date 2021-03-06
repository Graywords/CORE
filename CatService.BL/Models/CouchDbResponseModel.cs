﻿using Newtonsoft.Json;

namespace CatService.BL.Models
{
	public class CouchDbResponseModel
	{
		[JsonProperty("ok")]
		public bool Ok { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("rev")]
		public string Revision { get; set; }
	}
}
