using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBraverest.Models.API
{
    public class BasicDataDto
    {
        public string Colloq { get; set; }
        public bool ConsumeOnFull { get; set; }
        public bool Consumed { get; set; }
        public int Depth { get; set; }
        public string Description { get; set; }
        public List<string> From { get; set; }
        public GoldDto Gold { get; set; }
        public string Group { get; set; }
        public bool HideFromAll { get; set; }
        public int Id { get; set; }
        public ImageDto Image { get; set; }
        public bool InStore { get; set; }
        public List<string> Into { get; set; }
        public Dictionary<string, bool> Maps { get; set; }
        public string Name { get; set; }
        public string PlainText { get; set; }
        public string RequiredChampion { get; set; }
        public MetaDataDto Rune { get; set; }
        public string SanitizedDescription { get; set; }
        public int SpecialRecipe { get; set; }
        public int Stacks { get; set; }
        public BasicDataStatsDto Stats { get; set; }
        public List<string> Tags { get; set; }
    }
}
