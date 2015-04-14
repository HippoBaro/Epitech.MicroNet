using Newtonsoft.Json;

namespace Epitech.Intra.API.Data
{
	public class Trombi
	{
		[JsonProperty ("title")]
		public string FullName { get; set; }

		[JsonProperty ("type")]
		public string Type { get; set; }

		[JsonProperty ("login")]
		public string Login { get; set; }

		[JsonProperty ("picture")]
		public string Picture { get; set; }

		[JsonProperty ("course_code")]
		public string CourseCode { get; set; }

		[JsonProperty ("promo")]
		public string Promo { get; set; }

		[JsonProperty ("course")]
		public string Course { get; set; }
	}
}

