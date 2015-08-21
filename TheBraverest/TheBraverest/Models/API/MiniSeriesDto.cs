using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class MiniSeriesDto
    {
        public int Losses { get; set; }
        public string Progress { get; set; }
        public int Target { get; set; }
        public int Wins { get; set; }
    }
}