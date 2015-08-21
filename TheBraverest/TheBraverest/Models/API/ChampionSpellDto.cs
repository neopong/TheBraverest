using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class ChampionSpellDto
    {
        public ImageDto[] AltImages { get; set; }
        public double[] Cooldown { get; set; }
        public string CooldownBurn { get; set; }
        public int[] Cost { get; set; }
        public string CostBurn { get; set; }
        public string CostType { get; set; }
        public string Description { get; set; }
        public double?[][] Effect { get; set; }
        public string[] EffectBurn { get; set; }
        public ImageDto Image { get; set; }
        public string Key { get; set; }
        public LevelTipDto LevelTip { get; set; }
        public int MaxRank { get; set; }
        public string Name { get; set; }
        public object Range { get; set; }
        public string RangeBurn { get; set; }
        public string Resource { get; set; }
        public string SanitizedDescription { get; set; }
        public string SanitizedToolTip { get; set; }
        public string ToolTip { get; set; }
        public SpellVarsDto[] Vars { get; set; }
    }
}