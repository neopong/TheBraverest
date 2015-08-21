using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheBraverest.Classes;

namespace TheBraverest.Models.API
{
    public class Participant
    {
        public bool Bot { get; set; }
        public long ChampionId { get; set; }
        public string HighestAchievedSeasonTier { get; set; }
        public Mastery[] Masteries { get; set; }
        public int ParticipantId { get; set; }
        public int ProfileIconId { get; set; }
        public Rune[] Runes { get; set; }
        public long Spell1Id { get; set; }
        public long Spell2Id { get; set; }
        public ParticipantStats Stats { get; set; }
        public int SummonerId { get; set; }
        public string SummonerName { get; set; }
        public long TeamId { get; set; }
        public ParticipantTimeline Timeline { get; set; }
    }
}