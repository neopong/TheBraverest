using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBraverest.Models.API
{
    public class ItemListDto
    {
        public BasicDataDto Basic { get; set; }
        public Dictionary<string, ItemDto> Data { get; set; }
        public List<GroupDto> Groups { get; set; }
        public List<ItemTreeDto> Tree { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
    }
}
