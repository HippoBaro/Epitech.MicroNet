using Newtonsoft.Json;

namespace Epitech.Intra.API.Data.UserJsonTypes
{
	public class Telephone
	{
		[JsonProperty ("value")]
		public string Value { get; set; }
	}

}
