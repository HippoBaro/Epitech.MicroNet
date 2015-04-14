using Newtonsoft.Json;
using Epitech.Intra.API.Data.UserJsonTypes;

namespace Epitech.Intra.API.Data.UserJsonTypes
{
	public class Userinfo
	{
		[JsonProperty ("telephone")]
		public Telephone Telephone { get; set; }

		[JsonProperty ("email")]
		public Email Email { get; set; }

		[JsonProperty ("adresse")]
		public Email Adresse { get; set; }

		[JsonProperty ("ville")]
		public Email Ville { get; set; }

		[JsonProperty ("pays")]
		public Email Pays { get; set; }

		[JsonProperty ("poste")]
		public Email Poste { get; set; }

		[JsonProperty ("anniversaire")]
		public Email Anniversaire { get; set; }

		[JsonProperty ("website")]
		public Email Website { get; set; }
	}

}
