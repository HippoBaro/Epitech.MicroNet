using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.WelcomeJsonTypes;

namespace Epitech.Intra.API.Data.WelcomeJsonTypes
{

    public class Stage
    {

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("timeline_start")]
        public string TimelineStart { get; set; }

        [JsonProperty("timeline_end")]
        public string TimelineEnd { get; set; }

        [JsonProperty("timeline_barre")]
        public string TimelineBarre { get; set; }

        [JsonProperty("can_note")]
        public bool CanNote { get; set; }

        [JsonProperty("company_can_note")]
        public bool CompanyCanNote { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("mandatory")]
        public bool Mandatory { get; set; }
    }

}
