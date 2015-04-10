using System;

using Epitech.Intra.API;
using Newtonsoft.Json;

namespace Epitech.Intra.API.Data.ProjectJsonTypes
{
	public class Files
	{
		[JsonProperty("type")]
		public string type { get; set; }
		[JsonProperty("slug")]
		public string slug { get; set; }
		[JsonProperty("title")]
		public string title { get; set; }
		[JsonProperty("secure")]
		public bool secure { get; set; }
		[JsonProperty("synchro")]
		public bool synchro { get; set; }
		[JsonProperty("archive")]
		public bool archive { get; set; }
		[JsonProperty("language")]
		public string language { get; set; }
		[JsonProperty("size")]
		public int size { get; set; }
		[JsonProperty("ctime")]
		public string ctime { get; set; }
		[JsonProperty("mtime")]
		public string mtime { get; set; }
		[JsonProperty("mime")]
		public string mime { get; set; }
		[JsonProperty("isLeaf")]
		public bool isLeaf { get; set; }
		[JsonProperty("noFolder")]
		public bool noFolder { get; set; }
		[JsonProperty("fullpath")]
		public string fullpath { get; set; }

		public string Type {
			get {
				if (fullpath.EndsWith (".pdf"))
					return "Type : Document PDF";
				else if (fullpath.EndsWith (".mp4"))
					return "Type : Vidéo";
				else if (fullpath.EndsWith (".mp3") || fullpath.EndsWith (".wma"))
					return "Type : Audio";
				else if (fullpath.EndsWith (".html") || fullpath.EndsWith (".htm"))
					return "Type : Document HTML";
				else if (fullpath.EndsWith (".doc") || fullpath.EndsWith (".docx") || fullpath.EndsWith (".ppt") || fullpath.EndsWith (".pptx") || fullpath.EndsWith (".xls") || fullpath.EndsWith (".xlsx"))
					return "Type : Document Office";
				else if (fullpath.EndsWith (".zip") || fullpath.EndsWith (".rar") || fullpath.EndsWith (".tar") || fullpath.EndsWith (".gz") || fullpath.EndsWith (".7z") || fullpath.EndsWith (".bz2"))
					return "Type : Archive";
				else if (fullpath.EndsWith(".c") || fullpath.EndsWith(".cpp") || fullpath.EndsWith(".h") || fullpath.EndsWith(".py") || fullpath.EndsWith(".pl") || fullpath.EndsWith(".js"))
					return "Type : Source";
				else
					return "Type : Autres";
			}
			private set {
				Type = value;
			}
		}
	}
}

