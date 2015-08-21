using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class PlayerDto
    {
        public int ChampionId { get; set; }
        public int SummonerId { get; set; }
        public int TeamId { get; set; }
    }
}