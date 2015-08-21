using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class Player
    {
        public string MatchHistoryUri { get; set; }
        public int ProfileIcon { get; set; }
        public long SummonerId { get; set; }
        public string SummonerName { get; set; }
    }
}