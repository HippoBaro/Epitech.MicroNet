﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.EventJsonTypes;

namespace Epitech.Intra.API.Data.EventJsonTypes
{

    public class Room
    {

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("seats")]
        public int Seats { get; set; }
    }

}