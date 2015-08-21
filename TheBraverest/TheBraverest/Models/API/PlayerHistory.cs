using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class PlayerHistory
    {
        public MatchSummary[] Matches { get; set; }
    }
}