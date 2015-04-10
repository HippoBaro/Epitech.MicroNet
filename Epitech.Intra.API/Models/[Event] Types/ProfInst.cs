using System;
using Newtonsoft.Json;

namespace Epitech.Intra.API.EventJsonTypes
{
	public class ProfInst
	{
		[JsonProperty("type")]
		public string type { get; set; }
		[JsonProperty("login")]
		public string login { get; set; }
		[JsonProperty("title")]
		public string title { get; set; }
		[JsonProperty("picture")]
		public string picture { get; set; }
	}
}

