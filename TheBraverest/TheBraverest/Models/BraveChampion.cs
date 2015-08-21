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
        public List<ItemDto> Items { get; set; }
        public ChampionDto Champion { get; set; }
        public bool Success { get; set; }
    }
}
