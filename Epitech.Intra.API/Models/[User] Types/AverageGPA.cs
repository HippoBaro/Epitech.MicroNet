﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.UserJsonTypes;

namespace Epitech.Intra.API.Data.UserJsonTypes
{

    public class AverageGPA
    {

        [JsonProperty("cycle")]
        public string Cycle { get; set; }

        [JsonProperty("gpa_average")]
        public string GpaAverage { get; set; }
    }

}