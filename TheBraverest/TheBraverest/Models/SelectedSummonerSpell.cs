using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBraverest.Models
{
    /// <summary>
    /// Standard information for a summoner spell
    /// </summary>
    public class SelectedSummonerSpell : SelectedValue
    {
        /// <summary>
        /// The key for the summoner spell
        /// </summary>
        public string Key { get; set; }
    }
}
