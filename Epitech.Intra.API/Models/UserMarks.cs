using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.MarksJsonTypes;

namespace Epitech.Intra.API.Data
{

    public class UserMarks
    {

        public string TempStr { get; set; }

        [JsonProperty("modules")]
        public Module[] Modules { get; set; }

        [JsonProperty("notes")]
        public Note[] Notes { get; set; }
    }

}
