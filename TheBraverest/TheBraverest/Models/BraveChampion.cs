using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBraverest.Models.API;

namespace TheBraverest.Models
{
    public class BraveChampion
    {
        public int Seed { get; set; }
        public string Version { get; set; }
        public bool Success { get; set; }

        public SelectedValue Champion { get; set; }
        public SelectedValue Skill { get; set; }
        public List<SelectedItem> Items { get; set; }
        public List<SelectedValue> SummonerSpells { get; set; }
        public MasterySummary MasterySummary { get; set; }
    }
}
