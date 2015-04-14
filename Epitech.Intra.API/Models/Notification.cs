using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Epitech.Intra.API.Data
{
	public class NotifUser
	{
		[JsonProperty ("picture")]
		public string Picture { get; set; }

		[JsonProperty ("title")]
		public string Title { get; set; }

		[JsonProperty ("url")]
		public string Url { get; set; }
	}

	public class Notification
	{
		[JsonProperty ("title")]
		public string Title { get; set; }

		[JsonProperty ("user")]
		public NotifUser User { get; set; }

		[JsonProperty ("content")]
		public string Content { get; set; }

		[JsonProperty ("date")]
		public DateTime Date { get; set; }

		public List<HTMLCleaner.LinkItem> Links {
			get {
				return HTMLCleaner.GetLinks (Title); 
			}
		}

		public bool Unread { get; set; }
	}
}

