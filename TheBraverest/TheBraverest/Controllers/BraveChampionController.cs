using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Microsoft.Ajax.Utilities;
using TheBraverest.Classes;
using TheBraverest.Models;
using TheBraverest.Models.API;

namespace TheBraverest.Controllers
{
    public class BraveChampionController : ApiController
    {
        private static string apiKey = "04c3c3b3-fb27-483e-bc6b-87faaf62527a";

        // GET: api/BraveChampion
        [ResponseType(typeof(BraveChampion))]
        public async Task<IHttpActionResult> GetBraveChampion()
        {
            BraveChampion braveChampion = await CreateBraveChampion(null, null);

            if (!braveChampion.Success)
            {
                return NotFound();
            }

            return Ok(braveChampion);
        }

        // GET: api/BraveChampion
        [ResponseType(typeof(BraveChampion))]
        public async Task<IHttpActionResult> GetBraveChampion(string version, int seed)
        {
            BraveChampion braveChampion = await CreateBraveChampion(version, seed);

            if (!braveChampion.Success)
            {
                return NotFound();
            }

            return Ok(braveChampion);
        }

        private async Task<BraveChampion> CreateBraveChampion(string version, int? seed)
        {
            BraveChampion braveChampion = new BraveChampion();
            braveChampion.Seed = seed ?? (int) DateTime.Now.Ticks;

            bool champSuccess = false;
            bool itemSuccess = false;
            bool summonerSpellSuccess = false;
            bool hasSmite = false;
            bool isMelee = false;

            string versionAppend = "";

            //If specific version passed get data for that version
            if (!version.IsNullOrWhiteSpace())
            {
                versionAppend = "&version=" + version;
            }

            Random random = new Random(braveChampion.Seed);

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
            
            RESTResult<ItemListDto> itemResult;

            Dictionary<string, List<ItemDto>> allItemLists = new Dictionary<string, List<ItemDto>>();

            if (HttpRuntime.Cache.Get("ItemLists" + version ?? "") == null)
            {
                itemResult = await
                    RESTHelpers.RESTRequest<ItemListDto>("https://global.api.pvp.net/api/lol/static-data/na/v1.2/item",
                        "",
                        apiKey, "itemListData=all" + versionAppend);

                if (itemResult.Success)
                {
                    allItems = itemResult.ReturnObject.Data.Values.ToList();

                    selectableJungleItems =
                        itemResult.ReturnObject.Data.Values.Where(
                            i =>
                                i.Depth >= 3 && (i.Group == null || !i.Group.StartsWith("Boots")) &&
                                !i.Name.ToLower().Contains("hex core")).ToList();

                    jungleBootOptions =
                        itemResult.ReturnObject.Data.Values.Where(
                            i =>
                                i.Depth >= 3 && i.Group != null && i.Group.StartsWith("Boots") &&
                                !i.Name.ToLower().Contains("hex core")).ToList();

                    selectableNonJungleItems =
                        itemResult.ReturnObject.Data.Values.Where(
                            i =>
                                i.Depth >= 3 && (i.Group == null || !i.Group.StartsWith("Boots")) &&
                                i.Group != "JungleItems" && !i.Name.ToLower().Contains("hex core")).ToList();

                    nonJungleBootOptions =
                        itemResult.ReturnObject.Data.Values.Where(
                            i =>
                                i.Depth >= 3 && i.Group != null && i.Group.StartsWith("Boots") &&
                                i.Group != "JungleItems" && !i.Name.ToLower().Contains("hex core")).ToList();

                    allItemLists.Add("All", allItems);
                    allItemLists.Add("SelectableJungleItems", selectableJungleItems);
                    allItemLists.Add("JungleBootOptions", jungleBootOptions);
                    allItemLists.Add("SelectableItems", selectableNonJungleItems);
                    allItemLists.Add("BootOptions", nonJungleBootOptions);

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
                allItemLists = (Dictionary<string, List<ItemDto>>)HttpRuntime.Cache.Get("ItemLists" + version ?? "");
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

                for (int i = 0; i < 5; i++)
                {
                    do
                    {
                        selectedItemDto = selectableItems[random.Next(0, selectableItems.Count - 1)];
                    } while
                        (
                        //Ensure we haven't selected the item already
                        itemList.Any(si => si.Id == selectedItemDto.Id)
                        ||
                        //Don't allow melee champions to buy ranged only items
                        (isMelee && selectedItemDto.Name.Contains("(Ranged Only)"))
                        ||
                        //Don't allow ranged champions to buy melee only items
                        (!isMelee && selectedItemDto.Name.Contains("(Melee Only)"))
                        );

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


                braveChampion.Items = itemList;
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
            braveChampion.Success = champSuccess && itemSuccess && summonerSpellSuccess;

            return braveChampion;
        }
    }
}
