using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.ProjectJsonTypes;

namespace Epitech.Intra.API.Data.ProjectJsonTypes
{

    public class Notregistered
    {

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("promo")]
        public int Promo { get; set; }

        [JsonProperty("course_code")]
        public string CourseCode { get; set; }

        [JsonProperty("grade")]
        public string Grade { get; set; }

        [JsonProperty("cycle")]
        public string Cycle { get; set; }

        [JsonProperty("date_ins")]
        public string DateIns { get; set; }

        [JsonProperty("credits")]
        public int Credits { get; set; }

        [JsonProperty("flags")]
        public object[] Flags { get; set; }

        [JsonProperty("semester")]
        public string Semester { get; set; }
    }

}
