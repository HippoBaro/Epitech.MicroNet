using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Epitech.Intra.API.Data
{
	public class Step
	{
		[JsonProperty ("slugmodule")]
		public string slugmodule { get; set; }

		[JsonProperty ("disabled")]
		public string disabled { get; set; }

		[JsonProperty ("type")]
		public string type { get; set; }

		[JsonProperty ("stepcode")]
		public string stepcode { get; set; }

		[JsonProperty ("last_view")]
		public object last_view { get; set; }

		[JsonProperty ("titlestep")]
		public string titlestep { get; set; }

		[JsonProperty ("fullpath")]
		public string fullpath { get; set; }

		[JsonProperty ("current_language")]
		public string current_language { get; set; }

		[JsonProperty ("available_language")]
		public string available_language { get; set; }

		[JsonProperty ("srtpath")]
		public object srtpath { get; set; }

		[JsonProperty ("srtpath_fr")]
		public object srtpath_fr { get; set; }

		[JsonProperty ("srtpath_en")]
		public object srtpath_en { get; set; }

		[JsonProperty ("srtpath_cn")]
		public object srtpath_cn { get; set; }

		[JsonProperty ("current_language_srt")]
		public object current_language_srt { get; set; }

		[JsonProperty ("available_language_srt")]
		public string available_language_srt { get; set; }

		[JsonProperty ("forum_path")]
		public object forum_path { get; set; }

		[JsonProperty ("code")]
		public string code { get; set; }
	}

	public class StepIndex
	{
		[JsonProperty ("title")]
		public string title { get; set; }

		[JsonProperty ("step")]
		public Step step { get; set; }

		public string Type {
			get {
				if (step.fullpath.EndsWith(".pdf"))
					return "Type : Document PDF";
				else if (step.fullpath.EndsWith(".mp4"))
					return "Type : Vidéo";
				else if (step.fullpath.EndsWith(".mp3") ||step.fullpath.EndsWith(".wma"))
					return "Type : Audio";
				else if (step.fullpath.EndsWith(".html") || step.fullpath.EndsWith(".htm"))
					return "Type : Document HTML";
				else if (step.fullpath.EndsWith(".doc") || step.fullpath.EndsWith(".docx") || step.fullpath.EndsWith(".ppt") || step.fullpath.EndsWith(".pptx") || step.fullpath.EndsWith(".xls") || step.fullpath.EndsWith(".xlsx"))
					return "Type : Document Office";
				else if (step.fullpath.EndsWith(".zip") || step.fullpath.EndsWith(".rar") || step.fullpath.EndsWith(".tar") || step.fullpath.EndsWith(".gz") || step.fullpath.EndsWith(".7z") || step.fullpath.EndsWith(".bz2"))
					return "Type : Archive";
				else if (step.fullpath.EndsWith(".c") || step.fullpath.EndsWith(".cpp") || step.fullpath.EndsWith(".h") || step.fullpath.EndsWith(".py") || step.fullpath.EndsWith(".pl") || step.fullpath.EndsWith(".js"))
					return "Type : Source";
				else
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
		public string title { get; set; }

		[JsonProperty ("slug")]
		public string slug { get; set; }

		[JsonProperty ("type")]
		public string type { get; set; }

		[JsonProperty ("disabled")]
		public string disabled { get; set; }

		[JsonProperty ("steps")]
		public List<StepIndex> steps { get; set; }

		public int InnerCount {
			get {
				if (steps != null)
					return steps.Count;
				return 0;
			}
			private set {
				InnerCount = value;
			}
		}
	}

	public class ModuleElearning
	{
		[JsonProperty ("title")]
		public string title { get; set; }

		[JsonProperty ("slug")]
		public string slug { get; set; }

		[JsonProperty ("edit")]
		public string edit { get; set; }

		[JsonProperty ("classes")]
		public List<Class> classes { get; set; }

		public int InnerCount {
			get {
				if (classes != null)
					return classes.Count;
				return 0;
			}
			private set {
				InnerCount = value;
			}
		}
	}

	public class ELearning
	{
		[JsonProperty ("semester")]
		public int semester { get; set; }

		[JsonProperty ("modules")]
		public Dictionary<string, ModuleElearning> dic { get; set; }

		public int InnerCount {
			get {
				if (dic != null)
					return dic.Count;
				return 0;
			}
			private set {
				InnerCount = value;
			}
		}
	}
}

