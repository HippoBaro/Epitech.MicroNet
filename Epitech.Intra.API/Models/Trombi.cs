using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.CalendarJsonTypes;

namespace Epitech.Intra.API.Data
{
	public class Trombi
	{
		[JsonProperty("title")]
		public string FullName { get; set; }

		[JsonProperty("type")]
		public string type { get; set; }

		[JsonProperty("login")]
		public string login { get; set; }

		[JsonProperty("picture")]
		public string picture { get; set; }

		[JsonProperty("course_code")]
		public string course_code { get; set; }

		[JsonProperty("promo")]
		public string promo { get; set; }

		[JsonProperty("course")]
		public string course { get; set; }
	}
}

