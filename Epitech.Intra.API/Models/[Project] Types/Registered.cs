using Newtonsoft.Json;
using Epitech.Intra.API.Data.ProjectJsonTypes;

namespace Epitech.Intra.API.Data.ProjectJsonTypes
{
	public class Registered
	{
		[JsonProperty ("id")]
		public string Id { get; set; }

		[JsonProperty ("title")]
		public string Title { get; set; }

		[JsonProperty ("code")]
		public string Code { get; set; }

		[JsonProperty ("final_note")]
		public object FinalNote { get; set; }

		[JsonProperty ("repository")]
		public object Repository { get; set; }

		[JsonProperty ("closed")]
		public bool Closed { get; set; }

		[JsonProperty ("master")]
		public Master Master { get; set; }

		[JsonProperty ("members")]
		public Member[] Members { get; set; }
	}

}
