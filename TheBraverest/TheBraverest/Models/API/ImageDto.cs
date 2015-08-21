using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models
{
    public class ImageDto
    {
        public string Full { get; set; }
        public string Group { get; set; }
        public int H { get; set; }
        public string Sprite { get; set; }
        public int W { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}