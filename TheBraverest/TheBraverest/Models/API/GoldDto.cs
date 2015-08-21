using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBraverest.Models.API
{
    public class GoldDto
    {
        public int Base { get; set; }
        public bool Purchasable { get; set; }
        public int Sell { get; set; }
        public int Total { get; set; }
    }
}
