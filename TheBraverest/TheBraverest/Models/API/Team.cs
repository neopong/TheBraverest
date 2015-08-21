using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class Team
    {
        public BannedChampion[] Bans { get; set; }
        public int BaronKills { get; set; }
        public long DominionVictoryScore { get; set; }
        public int DragonKills { get; set; }
        public bool FirstBaron { get; set; }
        public bool FirstBlood { get; set; }
        public bool FirstDragon { get; set; }
        public bool FirstInhibitor { get; set; }
        public bool FirstTower { get; set; }
        public int InhibitorKills { get; set; }
        public int TeamId { get; set; }
        public int TowerKills { get; set; }
        public int VilemawKills { get; set; }
        public bool Winner { get; set; }
    }
}