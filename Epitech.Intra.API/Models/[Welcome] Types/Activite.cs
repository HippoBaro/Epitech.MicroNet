using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.WelcomeJsonTypes;

namespace Epitech.Intra.API.Data.WelcomeJsonTypes
{

    public class Activite
    {

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("module")]
        public string Module { get; set; }

        [JsonProperty("module_link")]
        public string ModuleLink { get; set; }

        [JsonProperty("module_code")]
        public string ModuleCode { get; set; }

        [JsonProperty("title_link")]
        public string TitleLink { get; set; }

        [JsonProperty("timeline_start")]
        public string TimelineStart { get; set; }

        [JsonProperty("timeline_end")]
        public string TimelineEnd { get; set; }

        [JsonProperty("timeline_barre")]
        public string TimelineBarre { get; set; }

        [JsonProperty("date_inscription")]
        public object DateInscription { get; set; }

        [JsonProperty("salle")]
        public string Salle { get; set; }

        [JsonProperty("intervenant")]
        public string Intervenant { get; set; }

        [JsonProperty("token")]
        public object Token { get; set; }

        [JsonProperty("token_link")]
        public string TokenLink { get; set; }

        [JsonProperty("register_link")]
        public string RegisterLink { get; set; }
    }

}
