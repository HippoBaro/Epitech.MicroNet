using System;

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Epitech.Intra.API.Data
{
	public class TrombiFilterItem
	{
		[JsonProperty ("title")]
		public string Title { get; set; }

		[JsonProperty ("login")]
		public string Login { get; set; }

		[JsonProperty ("nom")]
		public string Nom { get; set; }

		[JsonProperty ("prenom")]
		public string Prenom { get; set; }

		[JsonProperty ("picture")]
		public string Picture { get; set; }

		[JsonProperty ("location")]
		public string Location { get; set; }
	}

	public class TrombiFilter
	{
		[JsonProperty ("items")]
		public List<TrombiFilterItem> Results { get; set; }

		[JsonProperty ("total")]
		public int Count { get; set; }
	}
}