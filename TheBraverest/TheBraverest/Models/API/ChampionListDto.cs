using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class ChampionListDto
    {
        public Dictionary<string, ChampionDto> Data { get; set; }
        public string Format { get; set; }
        public Dictionary<string, string> Keys { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
    }
}