using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Epitech.Intra.API
{
	public class ActivityMark
	{
		[JsonProperty ("title")]
		public string Title { get; set; }

		[JsonProperty ("login")]
		public string Login { get; set; }

		[JsonProperty ("user_title")]
		public string UserTitle { get; set; }

		[JsonProperty ("picture")]
		public string Picture { get; set; }

		[JsonProperty ("all_members")]
		public object AllMembers { get; set; }

		[JsonProperty ("status")]
		public object Status { get; set; }

		[JsonProperty ("note")]
		public double Note { get; set; }

		[JsonProperty ("comment")]
		public string Comment { get; set; }

		[JsonProperty ("editable")]
		public bool Editable { get; set; }

		[JsonProperty ("type")]
		public string Type { get; set; }

		[JsonProperty ("grader")]
		public string Grader { get; set; }

		[JsonProperty ("date")]
		public string Date { get; set; }

		[JsonProperty ("group_title")]
		public string GroupTitle { get; set; }

		[JsonProperty ("group_master")]
		public string GroupMaster { get; set; }

		[JsonProperty ("member_status")]
		public string MemberStatus { get; set; }

		[JsonProperty ("members")]
		public List<object> Members { get; set; }
	}
}

