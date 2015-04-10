using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Epitech.Intra.API.Data
{

    public class Token
    {

        [JsonProperty("error")]
        public string Error { get; set; }
    }

}