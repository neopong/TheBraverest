using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
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
            BraveChampion braveChampion = new BraveChampion();
            braveChampion.Seed = (int)DateTime.Now.Ticks;

            bool champSuccess = false;
            bool itemSuccess = false;

            RESTResult<ChampionListDto> champs = await
                RESTHelpers.RESTRequest<ChampionListDto>(
                    "https://global.api.pvp.net/api/lol/static-data/na/v1.2/champion", "", apiKey, "champData=spells");

            Random random = new Random(braveChampion.Seed);

            if (champs.Success)
            {
                ChampionDto selectedChamp =
                    champs.ReturnObject.Data.Values[random.Next(0, champs.ReturnObject.Data.Values.Count - 1)];

                braveChampion.ChampionId = selectedChamp.Id;
                braveChampion.ChampionName = selectedChamp.Name;

                champSuccess = true;
            }

            RESTResult<ItemListDto> items = await
                RESTHelpers.RESTRequest<ItemListDto>("https://global.api.pvp.net/api/lol/static-data/na/v1.2/item", "",
                    apiKey, "itemListData=all");

            List<int> itemList = new List<int>();
            if (items.Success)
            {
                List<ItemDto> selectableItems = items.ReturnObject.Data.Values.Where(i => i.Depth >= 3).ToList();

                for (int i = 0; i < 6; i++)
                {
                    //TODO: Make it so it never selects the same item twice.  Also make it so always 1 max level boot and 5 depth 3 or greater non-boot items
                    itemList.Add(selectableItems[random.Next(0, selectableItems.Count - 1)].Id);
                }

                itemSuccess = true;

                braveChampion.Items = itemList.ToArray();
            }

            braveChampion.Success = champSuccess && itemSuccess;

            if (!braveChampion.Success)
            {
                return NotFound();
            }

            return Ok(braveChampion);
        }
    }
}
