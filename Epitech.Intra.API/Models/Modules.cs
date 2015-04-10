using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Epitech.Intra.API.Data
{
    public class Modules
    {
        public int ID { get; set; }
        public int ProgressMin { get; set; }
        public int ProgressMax { get; set; }
        public int ProgressValue { get; set; }
        public string Remaning { get; set; }

        [JsonProperty("title_module")]
        public string TitleModule { get; set; }

        [JsonProperty("codemodule")]
        public string Codemodule { get; set; }

        [JsonProperty("scolaryear")]
        public string Scolaryear { get; set; }

        [JsonProperty("codeinstance")]
        public string Codeinstance { get; set; }

        [JsonProperty("code_location")]
        public string CodeLocation { get; set; }

        [JsonProperty("begin_event")]
        public string BeginEvent { get; set; }

        [JsonProperty("end_event")]
        public string EndEvent { get; set; }

        [JsonProperty("seats")]
        public string Seats { get; set; }

        [JsonProperty("num_event")]
        public string NumEvent { get; set; }

        [JsonProperty("type_acti")]
        public string TypeActi { get; set; }

        [JsonProperty("type_acti_code")]
        public string TypeActiCode { get; set; }

        [JsonProperty("codeacti")]
        public string Codeacti { get; set; }

        [JsonProperty("acti_title")]
        public string ActiTitle { get; set; }

        [JsonProperty("num")]
        public string Num { get; set; }

        [JsonProperty("begin_acti")]
        public string BeginActi { get; set; }

        [JsonProperty("end_acti")]
        public string EndActi { get; set; }

        [JsonProperty("registered")]
        public int Registered { get; set; }

        [JsonProperty("info_creneau")]
        public object InfoCreneau { get; set; }

        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("rights")]
        public string[] Rights { get; set; }
    }

}
