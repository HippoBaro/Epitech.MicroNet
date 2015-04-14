using Newtonsoft.Json;

namespace Epitech.Intra.API.Data.UserJsonTypes
{
	public class Gpa
	{
		[JsonProperty ("gpa")]
		public double GPA { get; set; }

		[JsonProperty ("cycle")]
		public string Cycle { get; set; }
	}

}
