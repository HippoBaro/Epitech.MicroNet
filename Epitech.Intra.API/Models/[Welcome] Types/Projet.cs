using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.WelcomeJsonTypes;

namespace Epitech.Intra.API.Data.WelcomeJsonTypes
{

    public class Projet
    {

        [JsonProperty("title")]
        public string Title { get; set; }

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

        [JsonProperty("id_activite")]
        public string IdActivite { get; set; }

        [JsonProperty("soutenance_name")]
        public string SoutenanceName { get; set; }

        [JsonProperty("soutenance_link")]
        public string  SoutenanceLink { get; set; }

        [JsonProperty("soutenance_date")]
        public string  SoutenanceDate { get; set; }

        [JsonProperty("soutenance_salle")]
        public string SoutenanceSalle { get; set; }
    }

}
