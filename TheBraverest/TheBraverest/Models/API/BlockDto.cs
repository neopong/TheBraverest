using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class BlockDto
    {
        /// <summary>
        /// The name of the block as you would see it in the item set.
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// Use the tutorial formatting when displaying items in the block. 
        /// All items within the block are separated by a plus sign with the last item being separated by an arrow indicating that the other items build into the last item. 
        /// Defaults to false.
        /// </summary>
        public bool recMath { get; set; }
        /// <summary>
        /// The minimum required account level for the block to be visible to the player. Defaults to -1 which is equivalent to "any account level."
        /// </summary>
        public int minSummonerLevel { get; set; }
        /// <summary>
        /// The maximum allowed account level for the block to be visible to the player. Defaults to -1 which is equivalent to "any account level."
        /// </summary>
        public int maxSummonerLevel { get; set; }
        /// <summary>
        /// Only show the block if the player has equipped a specific summoner spell. Can be any valid summoner spell key, e.g. SummonerDot. 
        /// Defaults to an empty string. Will not override hideIfSummonerSpell.
        /// </summary>
        public string showIfSummonerSpell { get; set; }
        /// <summary>
        /// Hide the block if the player has equipped a specific summoner spell. Can be any valid summoner spell key, e.g. SummonerDot. 
        /// Defaults to an empty string. Overrides showIfSummonerSpell.
        /// </summary>
        public string hideIfSummonerSpell { get; set; }
        /// <summary>
        /// An array of items to be displayed within the block.
        /// </summary>
        public List<BlockItemDto> items { get; set; }
    }
}