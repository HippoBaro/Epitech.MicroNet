using Newtonsoft.Json;

namespace Epitech.Intra.API.Data
{
	public class Token
	{
		[JsonProperty ("error")]
		public string Error { get; set; }
	}

}