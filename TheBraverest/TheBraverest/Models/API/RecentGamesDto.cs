using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class RecentGamesDto
    {
        public GameDto[] Games { get; set; }
        public int SummonerId { get; set; }
    }
}