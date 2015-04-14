using Newtonsoft.Json;

namespace Epitech.Intra.API.Data.UserJsonTypes
{
	public class Group
	{
		[JsonProperty ("title")]
		public string Title { get; set; }

		[JsonProperty ("name")]
		public string Name { get; set; }

		[JsonProperty ("count")]
		public int Count { get; set; }
	}

}
