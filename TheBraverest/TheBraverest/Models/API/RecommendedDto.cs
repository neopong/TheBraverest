using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using TheBraverest.Controllers;

namespace TheBraverest.Models.API
{
    /// <summary>
    /// All details needed to serialize into a real item set in the game
    /// </summary>
    public class RecommendedDto
    {
        /// <summary>
        /// The name of the item set as you would see it in the drop down.
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Can be custom or global. 
        /// This field is only used for grouping and sorting item sets. Custom item sets are ordered above global item sets. 
        /// This field does not govern whether an item set available for every champion. 
        /// To make an item set available for every champion, the JSON file must be placed an item set in the global folder.
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// The map this item set will appear on. Can be any, Summoner's Rift SR, Howling Abyss HA, Twisted Treeline TT, or Crystal Scar CS.
        /// </summary>
        public string map { get; set; }
        /// <summary>
        /// The mode this item set will appear on. Can be any, CLASSIC, ARAM, or Dominion ODIN.
        /// </summary>
        public string mode { get; set; }
        /// <summary>
        /// Selectively sort this item set above other item sets. Overrides sortrank, but not type. Defaults to false.
        /// </summary>
        public bool priority { get; set; }
        /// <summary>
        /// The order in which this item set will be sorted within a specific type. Item sets are sorted in descending order.
        /// </summary>
        public int sortrank { get; set; }
        /// <summary>
        /// The sections within an item set.
        /// </summary>
        public List<BlockDto> blocks { get; set; }
        /// <summary>
        /// This is the Champion Key for the selected champion.
        /// It is not actually used in the JSON file but it is used to determine what directory to place the item set file in.
        /// The path is League of Legends\Config\Champions\{championKey}\Recommended\
        /// </summary>
        public string ChampionKey { get; set; }
        /// <summary>
        /// The Version of League of Legends that the request was originally made with.  This is used to recreate the BraveChampion or RecommendedDto.
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// The random seed that is used to regenerate the BraveChampion or RecommendedDto.
        /// </summary>
        public int Seed { get; set; }
        /// <summary>
        /// True if successfully created the RecommendedDto.
        /// This is not actually used in the JSON file.
        /// </summary>
        public bool Success { get; set; }

        internal static async Task<RecommendedDto> CreateRecommendedDto(string version, int? seed)
        {
            RecommendedDto recommendedDto = new RecommendedDto();

            BraveChampion braveChampion = await BraveChampion.Create(version, seed);

            if (!braveChampion.Success)
            {
                recommendedDto.Success = false;
            }
            else
            {

                recommendedDto.title =
                    string.Format("THE BRAVEREST {0} max {1} 1st with {2} + {3} - {4}/{5}/{6} Masteries",
                        braveChampion.Champion.Name, braveChampion.Skill.Name, braveChampion.SummonerSpells[0].Name,
                        braveChampion.SummonerSpells[1].Name,
                        braveChampion.MasterySummary.Offense, braveChampion.MasterySummary.Defense,
                        braveChampion.MasterySummary.Utility);

                recommendedDto.type = "custom";
                recommendedDto.map = "SR";
                recommendedDto.mode = "CLASSIC";
                recommendedDto.priority = true;
                recommendedDto.sortrank = 0;
                recommendedDto.ChampionKey = braveChampion.Key;
                recommendedDto.Version = braveChampion.Version;
                recommendedDto.Seed = braveChampion.Seed;

                recommendedDto.blocks = new List<BlockDto>();

                int itemIndex = 1;

                foreach (SelectedItem item in braveChampion.Items)
                {
                    BlockDto newBlock = new BlockDto();

                    switch (itemIndex)
                    {
                        case 1:
                            newBlock.type = "I'm a little brave (Buy 1st)";
                            break;
                        case 2:
                            newBlock.type = "I'm braver than most (Buy 2nd)";
                            break;
                        case 3:
                            newBlock.type = "Have you seen the size of these?! (Buy 3rd)";
                            break;
                        case 4:
                            newBlock.type = "I'm not a troll, I'm your master! (Buy 4th)";
                            break;
                        case 5:
                            newBlock.type = "Even Teemo trembles in my presence... (Buy 5th)";
                            break;
                        case 6:
                            newBlock.type = "I'M THE BRAVEREST!!1 (Buy Last and bust a seam)";
                            break;
                        default:
                            break;
                    }
                    newBlock.recMath = false;
                    newBlock.minSummonerLevel = -1;
                    newBlock.maxSummonerLevel = -1;

                    //If the item is a jungle item only show it if our supposed Brave Champion selected smite as they were supposed to
                    newBlock.showIfSummonerSpell = item.JungleItem ? "SummonerSmite" : "";

                    newBlock.hideIfSummonerSpell = "";

                    newBlock.items = new List<BlockItemDto>();
                    newBlock.items.Add(new BlockItemDto() { id = item.Id.ToString(), count = 1 });

                    recommendedDto.blocks.Add(newBlock);

                    itemIndex++;
                }

                recommendedDto.Success = true;
            }

            return recommendedDto;
        }
    }
}