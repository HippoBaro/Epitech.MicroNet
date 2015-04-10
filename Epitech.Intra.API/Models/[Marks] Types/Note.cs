using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epitech.Intra.API.Data.MarksJsonTypes;

namespace Epitech.Intra.API.Data.MarksJsonTypes
{

    public class Note
    {

        [JsonProperty("scolaryear")]
        public int Scolaryear { get; set; }

        [JsonProperty("codemodule")]
        public string Codemodule { get; set; }

        [JsonProperty("titlemodule")]
        public string Titlemodule { get; set; }

        [JsonProperty("codeinstance")]
        public string Codeinstance { get; set; }

        [JsonProperty("codeacti")]
        public string Codeacti { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("correcteur")]
        public string Correcteur { get; set; }

        [JsonProperty("final_note")]
        public double FinalNote { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }
    }

}
