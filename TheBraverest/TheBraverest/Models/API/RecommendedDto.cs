using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class RecommendedDto
    {
        public BlockDto[] Blocks { get; set; }
        public string Champion { get; set; }
        public string Map { get; set; }
        public string Mode { get; set; }
        public bool Priority { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }
}