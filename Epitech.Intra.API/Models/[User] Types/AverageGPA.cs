using Newtonsoft.Json;

namespace Epitech.Intra.API.Data.UserJsonTypes
{
	public class AverageGPA
	{
		[JsonProperty ("cycle")]
		public string Cycle { get; set; }

		[JsonProperty ("gpa_average")]
		public string GpaAverage { get; set; }
	}

}
