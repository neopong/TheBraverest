using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class SpellVarsDto
    {
        public double[] CoEff { get; set; }
        public string Dyn { get; set; }
        public string Key { get; set; }
        public string Link { get; set; }
        public string RanksWith { get; set; }
    }
}