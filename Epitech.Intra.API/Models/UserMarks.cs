using Newtonsoft.Json;
using Epitech.Intra.API.Data.MarksJsonTypes;

namespace Epitech.Intra.API.Data
{
	public class UserMarks
	{
		public string TempStr { get; set; }

		[JsonProperty ("modules")]
		public Module[] Modules { get; set; }

		[JsonProperty ("notes")]
		public Note[] Notes { get; set; }
	}

}
