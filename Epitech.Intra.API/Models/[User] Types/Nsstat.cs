﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.UserJsonTypes;

namespace Epitech.Intra.API.Data.UserJsonTypes
{

    public class Nsstat
    {

        [JsonProperty("active")]
        public double Active { get; set; }

        [JsonProperty("idle")]
        public double Idle { get; set; }

        [JsonProperty("out_active")]
        public double OutActive { get; set; }

        [JsonProperty("out_idle")]
        public double OutIdle { get; set; }

        [JsonProperty("nslog_norm")]
        public int? NslogNorm { get; set; }
    }

}
