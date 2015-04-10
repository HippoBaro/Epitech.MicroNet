using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.ProjectJsonTypes;

namespace Epitech.Intra.API.Data.ProjectJsonTypes
{

    public class Url
    {

        [JsonProperty("notation")]
        public bool Notation { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }
    }

}
