using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class LeagueDto
    {
        public LeagueEntryDto[] Entries { get; set; }
        public string Name { get; set; }
        public string ParticipantId { get; set; }
        public string Queue { get; set; }
        public string Tier { get; set; }
    }
}