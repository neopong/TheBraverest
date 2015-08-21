using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBraverest.Models.API
{
    public class ItemDto : BasicDataDto
    {
        public Dictionary<string, string> Effect { get; set; }
    }
}
