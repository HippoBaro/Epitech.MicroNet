using Newtonsoft.Json;
using System;

namespace Epitech.Intra.API.Data
{
	public class TokenResponse
	{
		[JsonProperty ("error")]
		public string Error { get; set; }
	}

	public class Token
	{
		public string TokenValue { get; set; }

		public int Rate { get; set; }

		public string Comment { get; set; }
	}

}