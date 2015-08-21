using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheBraverest.Models.API;

namespace TheBraverest.Models
{
    public class ChampionDto
    {
        public string[] AllyTips { get; set; }
        public string Blurb { get; set; }
        public string[] EnemyTips { get; set; }
        public int Id { get; set; }
        public ImageDto Image { get; set; }
        public InfoDto Info { get; set; }
        public string Key { get; set; }
        public string Lore { get; set; }
        public string Name { get; set; }
        public string Partype { get; set; }
        public PassiveDto Passive { get; set; }
        public RecommendedDto[] Recommended { get; set; }
        public SkinDto[] Skins { get; set; }
        public ChampionSpellDto[] Spells { get; set; }
        public StatsDto Stats { get; set; }
        public string[] Tags { get; set; }
        public string Title { get; set; }
    }
}