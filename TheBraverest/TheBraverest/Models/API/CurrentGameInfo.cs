using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class CurrentGameInfo
    {
        public BannedChampion[] BannedChampions { get; set; }
        public long GameId { get; set; }
        public string GameMode { get; set; }
        public long GameQueueConfigId { get; set; }
        public long GameStartTime { get; set; }
        public string GameType { get; set; }
        public long MapId { get; set; }
        public Observer Observers { get; set; }
        public Participant[] Participants { get; set; }
        public string PlatformId { get; set; }
    }
}