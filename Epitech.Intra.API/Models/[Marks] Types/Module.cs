using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.MarksJsonTypes;
using Xamarin.Forms;

namespace Epitech.Intra.API.Data.MarksJsonTypes
{
	public class Module
	{

		[JsonProperty ("scolaryear")]
		public int Scolaryear { get; set; }

		[JsonProperty ("id_user_history")]
		public string IdUserHistory { get; set; }

		[JsonProperty ("codemodule")]
		public string Codemodule { get; set; }

		[JsonProperty ("codeinstance")]
		public string Codeinstance { get; set; }

		[JsonProperty ("title")]
		public string Title { get; set; }

		[JsonProperty ("date_ins")]
		public string DateIns { get; set; }

		[JsonProperty ("cycle")]
		public string Cycle { get; set; }

		[JsonProperty ("grade")]
		public string Grade { get; set; }

		[JsonProperty ("credits")]
		public int Credits { get; set; }

		[JsonProperty ("barrage")]
		public int Barrage { get; set; }
	}

}
