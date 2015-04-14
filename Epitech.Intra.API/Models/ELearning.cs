using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Epitech.Intra.API.Data
{
	public class Step
	{
		[JsonProperty ("slugmodule")]
		public string Slugmodule { get; set; }

		[JsonProperty ("disabled")]
		public string Disabled { get; set; }

		[JsonProperty ("type")]
		public string Type { get; set; }

		[JsonProperty ("stepcode")]
		public string Stepcode { get; set; }

		[JsonProperty ("last_view")]
		public object LastView { get; set; }

		[JsonProperty ("titlestep")]
		public string Titlestep { get; set; }

		[JsonProperty ("fullpath")]
		public string Fullpath { get; set; }

		[JsonProperty ("current_language")]
		public string CurrentLanguage { get; set; }

		[JsonProperty ("available_language")]
		public string AvailableLanguage { get; set; }

		[JsonProperty ("srtpath")]
		public object Srtpath { get; set; }

		[JsonProperty ("srtpath_fr")]
		public object SrtpathFr { get; set; }

		[JsonProperty ("srtpath_en")]
		public object SrtpathEn { get; set; }

		[JsonProperty ("srtpath_cn")]
		public object SrtpathCn { get; set; }

		[JsonProperty ("current_language_srt")]
		public object CurrentLanguageSrt { get; set; }

		[JsonProperty ("available_language_srt")]
		public string AvailableLanguageSrt { get; set; }

		[JsonProperty ("forum_path")]
		public object ForumPath { get; set; }

		[JsonProperty ("code")]
		public string Code { get; set; }
	}

	public class StepIndex
	{
		[JsonProperty ("title")]
		public string Title { get; set; }

		[JsonProperty ("step")]
		public Step Step { get; set; }

		public string Type {
			get {
				if (Step.Fullpath.EndsWith (".pdf", StringComparison.Ordinal))
					return "Type : Document PDF";
				if (Step.Fullpath.EndsWith (".mp4", StringComparison.Ordinal))
					return "Type : Vidéo";
				if (Step.Fullpath.EndsWith (".mp3", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".wma", StringComparison.Ordinal))
					return "Type : Audio";
				if (Step.Fullpath.EndsWith (".html", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".htm", StringComparison.Ordinal))
					return "Type : Document HTML";
				if (Step.Fullpath.EndsWith (".doc", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".docx", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".ppt", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".pptx", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".xls", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".xlsx", StringComparison.Ordinal))
					return "Type : Document Office";
				if (Step.Fullpath.EndsWith (".zip", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".rar", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".tar", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".gz", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".7z", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".bz2", StringComparison.Ordinal))
					return "Type : Archive";
				if (Step.Fullpath.EndsWith (".c", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".cpp", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".h", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".py", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".pl", StringComparison.Ordinal) || Step.Fullpath.EndsWith (".js", StringComparison.Ordinal))
					return "Type : Source";
				return "Type : Autres";
			}
			private set {
				Type = value;
			}
		}

	}

	public class Class
	{
		[JsonProperty ("title")]
		public string Title { get; set; }

		[JsonProperty ("slug")]
		public string Slug { get; set; }

		[JsonProperty ("type")]
		public string Type { get; set; }

		[JsonProperty ("disabled")]
		public string Disabled { get; set; }

		[JsonProperty ("steps")]
		public List<StepIndex> Steps { get; set; }

		public int InnerCount {
			get {
				return Steps != null ? Steps.Count : 0;
			}
			private set {
				InnerCount = value;
			}
		}
	}

	public class ModuleElearning
	{
		[JsonProperty ("title")]
		public string Title { get; set; }

		[JsonProperty ("slug")]
		public string Slug { get; set; }

		[JsonProperty ("edit")]
		public string Edit { get; set; }

		[JsonProperty ("classes")]
		public List<Class> Classes { get; set; }

		public int InnerCount {
			get {
				return Classes != null ? Classes.Count : 0;
			}
			private set {
				InnerCount = value;
			}
		}
	}

	public class ELearning
	{
		[JsonProperty ("semester")]
		public int Semester { get; set; }

		[JsonProperty ("modules")]
		public Dictionary<string, ModuleElearning> Dic { get; set; }

		public int InnerCount {
			get {
				return Dic != null ? Dic.Count : 0;
			}
			private set {
				InnerCount = value;
			}
		}
	}
}

