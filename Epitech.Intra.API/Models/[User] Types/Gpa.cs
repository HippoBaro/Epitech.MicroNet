using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.UserJsonTypes;

namespace Epitech.Intra.API.Data.UserJsonTypes
{

    public class Gpa
    {

        [JsonProperty("gpa")]
        public double GPA { get; set; }

        [JsonProperty("cycle")]
        public string Cycle { get; set; }
    }

}
