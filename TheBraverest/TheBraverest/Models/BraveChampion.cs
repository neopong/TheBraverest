using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBraverest.Models.API;

namespace TheBraverest.Models
{
    /// <summary>
    /// This represents the set of all information to create an item set and random selection for Champions, Items, Skill and Mastery
    /// </summary>
    public class BraveChampion
    {
        /// <summary>
        /// The random seed that is used to regenerate the BraveChampion
        /// </summary>
        public int Seed { get; set; }
        /// <summary>
        /// The Version of League of Legends that the request was originally made with.  This is used to recreate the BraveChampion
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// True if the request was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The randomly selected Champion to play as
        /// </summary>
        public SelectedValue Champion { get; set; }
        /// <summary>
        /// The randomly selected Skill to max first
        /// </summary>
        public SelectedValue Skill { get; set; }
        /// <summary>
        /// The list of items to buy in order
        /// </summary>
        public List<SelectedItem> Items { get; set; }
        /// <summary>
        /// The random Summoner Spells to play with
        /// </summary>
        public List<SelectedValue> SummonerSpells { get; set; }
        /// <summary>
        /// The summary of Mastery distribution points
        /// </summary>
        public MasterySummary MasterySummary { get; set; }
    }
}
