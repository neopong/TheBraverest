using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class PassiveDto
    {
        public string Description { get; set; }
        public ImageDto Image { get; set; }
        public string Name { get; set; }
        public string SanitizedDescription { get; set; }
    }
}