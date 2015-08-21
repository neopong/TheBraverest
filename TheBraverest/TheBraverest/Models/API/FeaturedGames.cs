using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class FeaturedGames
    {
        public long ClientRefreshInterval { get; set; }
        public CurrentGameInfo[] GameList { get; set; }
        public string Region { get; set; }

        #region Model Extras
        public ChampionListDto Champions { get; set; }
        #endregion Model Extras
    }
}