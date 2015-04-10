using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Epitech.Intra.API.Data
{
	public class NotifUser
	{
		[JsonProperty ("picture")]
		public string picture { get; set; }

		[JsonProperty ("title")]
		public string title { get; set; }

		[JsonProperty ("url")]
		public string url { get; set; }
	}

	public class Notification
	{
		[JsonProperty ("title")]
		public string title { get; set; }

		[JsonProperty ("user")]
		public NotifUser user { get; set; }

		[JsonProperty ("content")]
		public string content { get; set; }

		[JsonProperty ("date")]
		public DateTime date { get; set; }

		public List<Epitech.Intra.API.HTMLCleaner.LinkItem> Links {
			get {
				return Epitech.Intra.API.HTMLCleaner.GetLinks (title); 
			}
		}

		public bool unread { get; set; }
	}
}

