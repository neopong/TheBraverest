using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBraverest.Models.API
{
    public class MapDetailsDto
    {
        public ImageDto Image { get; set; }
        public int MapId { get; set; }
        public string MapName { get; set; }
        public List<int> UnpurchasableItemList { get; set; }
    }
}
