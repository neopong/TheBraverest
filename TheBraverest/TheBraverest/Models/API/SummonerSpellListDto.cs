using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBraverest.Models.API
{
    class SummonerSpellListDto
    {
        public SortedList<string, SummonerSpellDto> Data { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
    }
}
