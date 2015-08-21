using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class Frame
    {
        public Event[] Events { get; set; }
        public Dictionary<string, ParticipantFrame> ParticipantFrames { get; set; }
        public long TimeStamp { get; set; }
    }
}