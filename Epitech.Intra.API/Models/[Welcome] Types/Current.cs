using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.WelcomeJsonTypes;

namespace Epitech.Intra.API.Data.WelcomeJsonTypes
{

    public class Current
    {

        [JsonProperty("active_log")]
        public string ActiveLog { get; set; }

        [JsonProperty("credits_min")]
        public string CreditsMin { get; set; }

        [JsonProperty("credits_norm")]
        public string CreditsNorm { get; set; }

        [JsonProperty("credits_obj")]
        public string CreditsObj { get; set; }

        [JsonProperty("nslog_min")]
        public string NslogMin { get; set; }

        [JsonProperty("nslog_norm")]
        public string NslogNorm { get; set; }

        [JsonProperty("semester_code")]
        public string SemesterCode { get; set; }

        [JsonProperty("semester_num")]
        public string SemesterNum { get; set; }

        [JsonProperty("achieved")]
        public int Achieved { get; set; }

        [JsonProperty("failed")]
        public int Failed { get; set; }

        [JsonProperty("inprogress")]
        public int Inprogress { get; set; }
    }

}
