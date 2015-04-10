using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.UserJsonTypes;

namespace Epitech.Intra.API.Data.UserJsonTypes
{
	public class NetsoulRawData
	{
		public int EpochTime { get; set; }
		public double TimeIldleScool { get; set; }
		public double TimeActiveScool { get; set; }
		public double TimeActiveOut { get; set; }
		public double TimeIldleOut { get; set; }
		public double Average { get; set; }
	}
}


