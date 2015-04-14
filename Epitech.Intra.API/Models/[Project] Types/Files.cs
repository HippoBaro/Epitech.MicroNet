using Newtonsoft.Json;

namespace Epitech.Intra.API.Data.ProjectJsonTypes
{
	public class Files
	{
		[JsonProperty ("type")]
		public string Type { get; set; }

		[JsonProperty ("slug")]
		public string Slug { get; set; }

		[JsonProperty ("title")]
		public string Title { get; set; }

		[JsonProperty ("secure")]
		public bool Secure { get; set; }

		[JsonProperty ("synchro")]
		public bool Synchro { get; set; }

		[JsonProperty ("archive")]
		public bool Archive { get; set; }

		[JsonProperty ("language")]
		public string Language { get; set; }

		[JsonProperty ("size")]
		public int Size { get; set; }

		[JsonProperty ("ctime")]
		public string Ctime { get; set; }

		[JsonProperty ("mtime")]
		public string Mtime { get; set; }

		[JsonProperty ("mime")]
		public string Mime { get; set; }

		[JsonProperty ("isLeaf")]
		public bool IsLeaf { get; set; }

		[JsonProperty ("noFolder")]
		public bool NoFolder { get; set; }

		[JsonProperty ("fullpath")]
		public string Fullpath { get; set; }

		public string TypeOfFile {
			get {
				if (Fullpath.EndsWith (".pdf", System.StringComparison.Ordinal))
					return "Type : Document PDF";
				else if (Fullpath.EndsWith (".mp4", System.StringComparison.Ordinal))
					return "Type : Vidéo";
				else if (Fullpath.EndsWith (".mp3", System.StringComparison.Ordinal) || Fullpath.EndsWith (".wma", System.StringComparison.Ordinal))
					return "Type : Audio";
				else if (Fullpath.EndsWith (".html", System.StringComparison.Ordinal) || Fullpath.EndsWith (".htm", System.StringComparison.Ordinal))
					return "Type : Document HTML";
				else if (Fullpath.EndsWith (".doc", System.StringComparison.Ordinal) || Fullpath.EndsWith (".docx", System.StringComparison.Ordinal) || Fullpath.EndsWith (".ppt", System.StringComparison.Ordinal) || Fullpath.EndsWith (".pptx", System.StringComparison.Ordinal) || Fullpath.EndsWith (".xls", System.StringComparison.Ordinal) || Fullpath.EndsWith (".xlsx", System.StringComparison.Ordinal))
					return "Type : Document Office";
				else if (Fullpath.EndsWith (".zip", System.StringComparison.Ordinal) || Fullpath.EndsWith (".rar", System.StringComparison.Ordinal) || Fullpath.EndsWith (".tar", System.StringComparison.Ordinal) || Fullpath.EndsWith (".gz", System.StringComparison.Ordinal) || Fullpath.EndsWith (".7z", System.StringComparison.Ordinal) || Fullpath.EndsWith (".bz2", System.StringComparison.Ordinal))
					return "Type : Archive";
				else if (Fullpath.EndsWith (".c", System.StringComparison.Ordinal) || Fullpath.EndsWith (".cpp", System.StringComparison.Ordinal) || Fullpath.EndsWith (".h", System.StringComparison.Ordinal) || Fullpath.EndsWith (".py", System.StringComparison.Ordinal) || Fullpath.EndsWith (".pl", System.StringComparison.Ordinal) || Fullpath.EndsWith (".js", System.StringComparison.Ordinal))
					return "Type : Source";
				else
					return "Type : Autres";
			}
			private set {
				TypeOfFile = value;
			}
		}
	}
}

