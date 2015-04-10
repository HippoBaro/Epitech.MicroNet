using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.WelcomeJsonTypes;

namespace Epitech.Intra.API.Data.WelcomeJsonTypes
{

    public class Board
    {

        [JsonProperty("projets")]
        public Projet[] Projets { get; set; }

        [JsonProperty("notes")]
        public object[] Notes { get; set; }

        [JsonProperty("susies")]
        public object[] Susies { get; set; }

        [JsonProperty("activites")]
        public Activite[] Activites { get; set; }

        [JsonProperty("modules")]
        public Module[] Modules { get; set; }

        [JsonProperty("stages")]
        public Stage[] Stages { get; set; }

        [JsonProperty("tickets")]
        public object[] Tickets { get; set; }
    }

}
