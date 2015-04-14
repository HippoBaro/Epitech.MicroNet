using Newtonsoft.Json;

namespace Epitech.Intra.API.Data.CalendarJsonTypes
{
	public class ProfInst
	{
		[JsonProperty ("type")]
		public string Type { get; set; }

		[JsonProperty ("login")]
		public string Login { get; set; }

		[JsonProperty ("title")]
		public string Title { get; set; }

		[JsonProperty ("picture")]
		public string Picture { get; set; }
	}

}
