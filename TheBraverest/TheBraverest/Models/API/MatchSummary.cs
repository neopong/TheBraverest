using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class MatchSummary
    {
        public int MapId { get; set; }
        public long MatchCreation { get; set; }
        public long MatchDuration { get; set; }
        public long MatchId { get; set; }
        public string MatchMode { get; set; }
        public string MatchType { get; set; }
        public string MatchVersion { get; set; }
        public ParticipantIdentity[] ParticipantIdentities { get; set; }
        public Participant[] Participants { get; set; }
        public string PlatformId { get; set; }
        public string QueueType { get; set; }
        public string Region { get; set; }
        public string Season { get; set; }
    }
}