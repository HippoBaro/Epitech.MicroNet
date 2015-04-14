using Newtonsoft.Json;

namespace Epitech.Intra.API.Data.UserJsonTypes
{
	public class Email
	{
		[JsonProperty ("value")]
		public string Value { get; set; }
	}

}
