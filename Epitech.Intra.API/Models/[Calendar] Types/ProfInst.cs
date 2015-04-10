using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.CalendarJsonTypes;

namespace Epitech.Intra.API.Data.CalendarJsonTypes
{

    public class ProfInst
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }
    }

}
