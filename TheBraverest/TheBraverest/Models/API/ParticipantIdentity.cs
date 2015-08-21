using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class ParticipantIdentity
    {
        public int ParticipantId { get; set; }
        public Player Player { get; set; }
    }
}