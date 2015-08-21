using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class ProfileIconList
    {
        public string Type { get; set; }
        public string Version { get; set; }
        public Dictionary<string, ProfileIcon> Data { get; set; }
    }
}