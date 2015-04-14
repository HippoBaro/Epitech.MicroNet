using Newtonsoft.Json;
using Epitech.Intra.API.Data.WelcomeJsonTypes;

namespace Epitech.Intra.API.Data
{
	public class Welcome
	{
		[JsonProperty ("ip")]
		public string Ip { get; set; }

		[JsonProperty ("board")]
		public Board Board { get; set; }

		[JsonProperty ("history")]
		public History[] History { get; set; }

		[JsonProperty ("infos")]
		public Infos Infos { get; set; }

		[JsonProperty ("current")]
		public Current Current { get; set; }
	}

}
