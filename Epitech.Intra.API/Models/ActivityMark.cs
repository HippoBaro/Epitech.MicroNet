using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Epitech.Intra.API
{
	public class ActivityMark
	{
		[JsonProperty ("title")]
		public string title { get; set; }

		[JsonProperty ("login")]
		public string login { get; set; }

		[JsonProperty ("user_title")]
		public string user_title { get; set; }

		[JsonProperty ("picture")]
		public string picture { get; set; }

		[JsonProperty ("all_members")]
		public object all_members { get; set; }

		[JsonProperty ("status")]
		public object status { get; set; }

		[JsonProperty ("note")]
		public double note { get; set; }

		[JsonProperty ("comment")]
		public string comment { get; set; }

		[JsonProperty ("editable")]
		public bool editable { get; set; }

		[JsonProperty ("type")]
		public string type { get; set; }

		[JsonProperty ("grader")]
		public string grader { get; set; }

		[JsonProperty ("date")]
		public string date { get; set; }

		[JsonProperty ("group_title")]
		public string group_title { get; set; }

		[JsonProperty ("group_master")]
		public string group_master { get; set; }

		[JsonProperty ("member_status")]
		public string member_status { get; set; }

		[JsonProperty ("members")]
		public List<object> members { get; set; }
	}
}

