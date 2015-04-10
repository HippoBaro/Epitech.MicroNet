using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.WelcomeJsonTypes;

namespace Epitech.Intra.API.Data.WelcomeJsonTypes
{

    public class History
    {

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("visible")]
        public string Visible { get; set; }

        [JsonProperty("id_activite")]
        public string IdActivite { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }
    }

}
