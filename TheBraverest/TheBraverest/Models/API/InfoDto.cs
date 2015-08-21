using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class InfoDto
    {
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Difficulty { get; set; }
        public int Magic { get; set; }
    }
}