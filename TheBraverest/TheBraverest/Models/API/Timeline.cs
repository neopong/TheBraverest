using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class Timeline
    {
        public long FrameInterval { get; set; }
        public Frame[] Frames { get; set; }
    }
}