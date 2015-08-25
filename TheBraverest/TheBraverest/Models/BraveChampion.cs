using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using Microsoft.Ajax.Utilities;
using TheBraverest.Classes;
using TheBraverest.Models.API;

namespace TheBraverest.Models
{
    /// <summary>
    /// This represents the set of all information to create an item set and random selection for Champions, Items, Skill and Mastery
    /// </summary>
    public class BraveChampion
    {
        private static string apiKey = "04c3c3b3-fb27-483e-bc6b-87faaf62527a";

        /// <summary>
        /// The random seed that is used to regenerate the BraveChampion or RecommendedDto.
        /// </summary>
        public int Seed { get; set; }
        /// <summary>
        /// The Version of League of Legends that the request was originally made with.  This is used to recreate the BraveChampion or RecommendedDto.
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// The Champion Key.  This is used to determine the directory to drop the item set file.
        /// </summary>
        public string Key { get; set; }
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


        internal static async Task<BraveChampion> Create(string version, int? seed)
        {
            BraveChampion braveChampion = new BraveChampion();
            braveChampion.Seed = seed ?? (int)DateTime.Now.Ticks;

            bool champSuccess = false;
            bool itemSuccess = false;
            bool summonerSpellSuccess = false;
            bool hasSmite = false;
            bool isMelee = false;
            bool mapSuccess = false;

            string versionAppend = "";

            //If specific version passed get data for that version
            if (!version.IsNullOrWhiteSpace())
            {
                versionAppend = "&version=" + version;
            }

            Random random = new Random(braveChampion.Seed);

            #region Pull Map Data
            RESTResult<MapDataDto> mapData;

            if (HttpRuntime.Cache.Get("MapData" + version ?? "") == null)
            {
                mapData = await
                    RESTHelpers.RESTRequest<MapDataDto>(
                        "https://global.api.pvp.net/api/lol/static-data/na/v1.2/map", "", apiKey, versionAppend);


                if (mapData.Success)
                {
                    HttpRuntime.Cache.Add
                    (
                        "MapData" + version ?? "",
                        mapData,
                        null,
                        DateTime.Now.AddDays(1.0),
                        Cache.NoSlidingExpiration,
                        CacheItemPriority.NotRemovable,
                        null
                    );

                    mapSuccess = true;
                }
            }
            else
            {
                mapData = (RESTResult<MapDataDto>)HttpRuntime.Cache.Get("MapData" + version ?? "");
                mapSuccess = true;
            }
            #endregion Pull Map Data

            #region Champion Selection
            RESTResult<ChampionListDto> champs;

            if (HttpRuntime.Cache.Get("ChampList" + version ?? "") == null)
            {
                champs = await
                    RESTHelpers.RESTRequest<ChampionListDto>(
                        "https://global.api.pvp.net/api/lol/static-data/na/v1.2/champion", "", apiKey, "champData=all" + versionAppend);


                if (champs.Success)
                {
                    HttpRuntime.Cache.Add
                    (
                        "ChampList" + version ?? "",
                        champs,
                        null,
                        DateTime.Now.AddDays(1.0),
                        Cache.NoSlidingExpiration,
                        CacheItemPriority.NotRemovable,
                        null
                    );
                }
            }
            else
            {
                champs = (RESTResult<ChampionListDto>)HttpRuntime.Cache.Get("ChampList" + version ?? "");
            }

            if (champs.Success)
            {
                ChampionDto selectedChamp =
                    champs.ReturnObject.Data.Values[random.Next(0, champs.ReturnObject.Data.Values.Count - 1)];

                braveChampion.Version = champs.ReturnObject.Version;
                braveChampion.Key = selectedChamp.Key;

                braveChampion.Champion = new SelectedValue()
                {
                    Id = selectedChamp.Id,
                    ImageUrl =
                        string.Format("http://ddragon.leagueoflegends.com/cdn/{0}/img/champion/{1}",
                            braveChampion.Version, selectedChamp.Image.Full),
                    Name = selectedChamp.Name
                };

                //Champions with an attack range less than or equal to 200 are melee.  This determines if they can have melee or range only items.
                if (selectedChamp.Stats.AttackRange <= 200)
                {
                    isMelee = true;
                }

                #region Skill Selection
                int skillNum = random.Next(0, 3);

                ChampionSpellDto selectedSkill = selectedChamp.Spells[skillNum];

                braveChampion.Skill = new SelectedValue()
                {
                    Id = skillNum,
                    ImageUrl =
                        string.Format("http://ddragon.leagueoflegends.com/cdn/{0}/img/spell/{1}",
                            braveChampion.Version, selectedSkill.Image.Full),
                    Name = selectedSkill.Name
                };
                #endregion Skill Selection

                champSuccess = true;
            }
            #endregion Champion Selection

            #region Summoner Spell Selection
            RESTResult<SummonerSpellListDto> summonerSpells;

            if (HttpRuntime.Cache.Get("SummonerSpellList" + version ?? "") == null)
            {
                summonerSpells = await
                    RESTHelpers.RESTRequest<SummonerSpellListDto>(
                        "https://global.api.pvp.net/api/lol/static-data/na/v1.2/summoner-spell", "", apiKey,
                        "spellData=all" + versionAppend);


                if (summonerSpells.Success)
                {
                    HttpRuntime.Cache.Add
                    (
                        "SummonerSpellList" + version ?? "",
                        summonerSpells,
                        null,
                        DateTime.Now.AddDays(1.0),
                        Cache.NoSlidingExpiration,
                        CacheItemPriority.NotRemovable,
                        null
                    );
                }
            }
            else
            {
                summonerSpells = (RESTResult<SummonerSpellListDto>)HttpRuntime.Cache.Get("SummonerSpellList" + version ?? "");
            }

            if (summonerSpells.Success)
            {
                List<SummonerSpellDto> possibleSummonerSpells =
                    summonerSpells.ReturnObject.Data.Values.Where(s => s.Modes.Contains("CLASSIC")).ToList();

                SummonerSpellDto selectedSummonerSpell =
                    possibleSummonerSpells[random.Next(0, possibleSummonerSpells.Count - 1)];

                if (selectedSummonerSpell.Id == 11)
                {
                    hasSmite = true;
                }

                braveChampion.SummonerSpells = new List<SelectedValue>();

                braveChampion.SummonerSpells.Add(new SelectedValue()
                {
                    Id = selectedSummonerSpell.Id,
                    ImageUrl =
                        string.Format("http://ddragon.leagueoflegends.com/cdn/{0}/img/spell/{1}",
                            braveChampion.Version, selectedSummonerSpell.Image.Full),
                    Name = selectedSummonerSpell.Name
                });

                do
                {
                    selectedSummonerSpell = possibleSummonerSpells[random.Next(0, possibleSummonerSpells.Count - 1)];
                } while (braveChampion.SummonerSpells.Any(s => s.Id == selectedSummonerSpell.Id));

                if (selectedSummonerSpell.Id == 11)
                {
                    hasSmite = true;
                }

                braveChampion.SummonerSpells.Add(new SelectedValue()
                {
                    Id = selectedSummonerSpell.Id,
                    ImageUrl =
                        string.Format("http://ddragon.leagueoflegends.com/cdn/{0}/img/spell/{1}",
                            braveChampion.Version, selectedSummonerSpell.Image.Full),
                    Name = selectedSummonerSpell.Name
                });

                summonerSpellSuccess = true;
            }
            #endregion Summoner Spell Selection

            #region Item Selection
            List<SelectedItem> itemList = new List<SelectedItem>();

            List<ItemDto> selectableItems;
            List<ItemDto> selectableNonJungleItems;
            List<ItemDto> selectableJungleItems;
            List<ItemDto> bootOptions;
            List<ItemDto> nonJungleBootOptions;
            List<ItemDto> jungleBootOptions;
            List<ItemDto> allItems;
            List<ItemDto> jungleOnlyItems;

            RESTResult<ItemListDto> itemResult;

            Dictionary<string, List<ItemDto>> allItemLists = new Dictionary<string, List<ItemDto>>();

            if (mapSuccess)
            {
                if (HttpRuntime.Cache.Get("ItemLists" + version ?? "") == null)
                {
                    itemResult = await
                        RESTHelpers.RESTRequest<ItemListDto>(
                            "https://global.api.pvp.net/api/lol/static-data/na/v1.2/item",
                            "",
                            apiKey, "itemListData=all" + versionAppend);

                    if (itemResult.Success)
                    {
                        //Don't include Bilgewater event items and items that are not allowed on the Rift
                        //There might be a better way to exclude Bilgewater items but I can't find it.  Is there a flag?
                        allItems =
                            itemResult.ReturnObject.Data.Values.Where(
                                i =>
                                    (i.Tags == null || !i.Tags.Contains("Bilgewater")) &&
                                    !mapData.ReturnObject.Data["11"].UnpurchasableItemList.Any(id => id == i.Id))
                                .ToList();

                        selectableJungleItems =
                            allItems.Where(
                                i =>
                                    i.Depth >= 3 && (i.Group == null || !i.Group.StartsWith("Boots")) &&
                                    !i.Name.ToLower().Contains("hex core") && i.Into == null).ToList();

                        //Get all boots but not in group BootsTeleport as not valid for map.
                        //There might be a better way to exclude Bilgewater items but I can't find it.  Is there a flag?
                        jungleBootOptions =
                            allItems.Where(
                                i =>
                                    i.Depth >= 3 && i.Group != null && i.Group.StartsWith("Boots") &&
                                    i.Group != "BootsTeleport" &&
                                    !i.Name.ToLower().Contains("hex core") && i.Into == null).ToList();

                        selectableNonJungleItems =
                            allItems.Where(
                                i =>
                                    i.Depth >= 3 && (i.Group == null || !i.Group.StartsWith("Boots")) &&
                                    i.Group != "JungleItems" && !i.Name.ToLower().Contains("hex core") && i.Into == null)
                                .ToList();

                        //Get all boots but not in group BootsTeleport as not valid for map.
                        //There might be a better way to exclude Bilgewater items but I can't find it.  Is there a flag?
                        nonJungleBootOptions =
                            allItems.Where(
                                i =>
                                    i.Depth >= 3 && i.Group != null && i.Group.StartsWith("Boots") &&
                                    i.Group != "BootsTeleport" &&
                                    i.Group != "JungleItems" && !i.Name.ToLower().Contains("hex core") && i.Into == null)
                                .ToList();

                        jungleOnlyItems = allItems.Where(i => i.Group == "JungleItems").ToList();

                        allItemLists.Add("All", allItems);
                        allItemLists.Add("SelectableJungleItems", selectableJungleItems);
                        allItemLists.Add("JungleBootOptions", jungleBootOptions);
                        allItemLists.Add("SelectableItems", selectableNonJungleItems);
                        allItemLists.Add("BootOptions", nonJungleBootOptions);
                        allItemLists.Add("JungleOnlyItems", jungleOnlyItems);

                        HttpRuntime.Cache.Add
                            (
                                "ItemLists" + version ?? "",
                                allItemLists,
                                null,
                                DateTime.Now.AddDays(1.0),
                                Cache.NoSlidingExpiration,
                                CacheItemPriority.NotRemovable,
                                null
                            );

                        itemSuccess = true;
                    }
                }
                else
                {
                    allItemLists =
                        (Dictionary<string, List<ItemDto>>)HttpRuntime.Cache.Get("ItemLists" + version ?? "");
                    itemSuccess = true;
                }


                if (itemSuccess)
                {
                    if (hasSmite)
                    {
                        selectableItems = allItemLists["SelectableJungleItems"];
                        bootOptions = allItemLists["JungleBootOptions"];
                    }
                    else
                    {
                        selectableItems = allItemLists["SelectableItems"];
                        bootOptions = allItemLists["BootOptions"];
                    }

                    jungleOnlyItems = allItemLists["JungleOnlyItems"];

                    allItems = allItemLists["All"];

                    ItemDto selectedItemDto;
                    ItemDto relatedBoot;

                    //Non-viktor builds get boots
                    if (braveChampion.Champion.Id != 112)
                    {
                        selectedItemDto = bootOptions[random.Next(0, bootOptions.Count - 1)];
                        relatedBoot = allItems.FirstOrDefault(b => b.Id.ToString() == selectedItemDto.From[0]);

                        itemList.Add(new SelectedItem()
                        {
                            Cost = selectedItemDto.Gold.Total,
                            Id = selectedItemDto.Id,
                            ImageUrl =
                                string.Format("http://ddragon.leagueoflegends.com/cdn/{0}/img/item/{1}",
                                    braveChampion.Version, selectedItemDto.Image.Full),
                            Name = string.Format("{0} - {1}", relatedBoot.Name, selectedItemDto.Name)
                        });
                    }
                    //Viktor builds get the Perfect Hex Core
                    else
                    {
                        selectedItemDto = allItems.FirstOrDefault(i => i.Name == "Perfect Hex Core");

                        itemList.Add(new SelectedItem()
                        {
                            Cost = selectedItemDto.Gold.Total,
                            Id = selectedItemDto.Id,
                            ImageUrl =
                                string.Format("http://ddragon.leagueoflegends.com/cdn/{0}/img/item/{1}",
                                    braveChampion.Version, selectedItemDto.Image.Full),
                            Name = selectedItemDto.Name
                        });
                    }

                    bool hasJungleItem = false;

                    for (int i = 0; i < 5; i++)
                    {
                        bool isJungleItem;
                        do
                        {
                            selectedItemDto = selectableItems[random.Next(0, selectableItems.Count - 1)];

                            if (hasSmite && selectedItemDto.Group == "JungleItems")
                            {
                                isJungleItem = true;
                            }
                            else
                            {
                                isJungleItem = false;
                            }
                            //TODO: Link jungle item enchant to base jungle item for more clarity in the name
                        } while
                            (
                            //Ensure we haven't selected the item already
                            itemList.Any(si => si.Id == selectedItemDto.Id)
                            ||
                            //Don't allow melee champions to buy ranged only items.  Might be a better way to do this but I don't see it.  Is there an actual flag?
                            (isMelee && selectedItemDto.Name.Contains("(Ranged Only)"))
                            ||
                            //Don't allow ranged champions to buy melee only items.  Might be a better way to do this but I don't see it.  Is there an actual flag?
                            (!isMelee && selectedItemDto.Name.Contains("(Melee Only)"))
                            ||
                            //Don't allow more than one jungle item to be added
                            (hasJungleItem && isJungleItem)
                            );

                        string itemName = selectedItemDto.Name;

                        //Append base jungle item name to enchantment like we do with boots for jungle items
                        if (isJungleItem)
                        {
                            hasJungleItem = true;
                            ItemDto baseJungleItem;

                            foreach (string itemId in selectedItemDto.From)
                            {

                                baseJungleItem = jungleOnlyItems.FirstOrDefault(ji => ji.Id == Convert.ToInt32(itemId));

                                if (baseJungleItem != null)
                                {
                                    itemName = string.Format("{0} - {1}", baseJungleItem.Name, selectedItemDto.Name);
                                    break;
                                }
                            }
                        }

                        itemList.Add(new SelectedItem()
                        {
                            Cost = selectedItemDto.Gold.Total,
                            Id = selectedItemDto.Id,
                            ImageUrl =
                                string.Format("http://ddragon.leagueoflegends.com/cdn/{0}/img/item/{1}",
                                    braveChampion.Version, selectedItemDto.Image.Full),
                            Name = itemName,
                            JungleItem = isJungleItem
                        });
                    }

                    braveChampion.Items = itemList;
                }
            }
            #endregion Item Selection

            #region Mastery Summary Selection
            int totalMasteries = 0;
            int selectedMasteryTotal = 0;

            braveChampion.MasterySummary = new MasterySummary();
            selectedMasteryTotal = random.Next(0, 31);
            totalMasteries = selectedMasteryTotal;
            braveChampion.MasterySummary.Offense = selectedMasteryTotal;
            selectedMasteryTotal = random.Next(0, 31 - totalMasteries);
            totalMasteries += selectedMasteryTotal;
            braveChampion.MasterySummary.Defense = selectedMasteryTotal;
            braveChampion.MasterySummary.Utility = 30 - totalMasteries;
            #endregion Mastery Summary Selection

            //Only successful if got champion, item and summoner spell data.
            braveChampion.Success = mapSuccess && champSuccess && itemSuccess && summonerSpellSuccess;

            return braveChampion;
        }

    }
}
