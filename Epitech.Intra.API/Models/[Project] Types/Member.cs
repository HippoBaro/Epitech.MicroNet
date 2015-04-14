using Newtonsoft.Json;

namespace Epitech.Intra.API.Data.ProjectJsonTypes
{
	public class Member
	{
		[JsonProperty ("login")]
		public string Login { get; set; }

		[JsonProperty ("status")]
		public string Status { get; set; }

		[JsonProperty ("picture")]
		public string Picture { get; set; }

		[JsonProperty ("title")]
		public string Title { get; set; }
	}

}
