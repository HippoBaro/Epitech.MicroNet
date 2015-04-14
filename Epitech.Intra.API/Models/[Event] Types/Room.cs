using Newtonsoft.Json;

namespace Epitech.Intra.API.Data.EventJsonTypes
{
	public class Room
	{
		[JsonProperty ("code")]
		public string Code { get; set; }

		[JsonProperty ("type")]
		public string Type { get; set; }

		[JsonProperty ("seats")]
		public int Seats { get; set; }
	}

}
