using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    public class Event
    {
        public string AscendedType { get; set; }
        public int[] AssistingParticipantIds { get; set; }
        public string BuildingType { get; set; }
        public int CreatorId { get; set; }
        public string EventType { get; set; }
        public int ItemAfter { get; set; }
        public int ItemBefore { get; set; }
        public int ItemId { get; set; }
        public int KillerId { get; set; }
        public string LaneType { get; set; }
        public string LevelUpType { get; set; }
        public string MonsterType { get; set; }
        public int ParticipantId { get; set; }
        public string PointCaptured { get; set; }
        public Position Position { get; set; }
        public int SkillSlot { get; set; }
        public int TeamId { get; set; }
        public long TimeStamp { get; set; }
        public string TowerType { get; set; }
        public int VictimId { get; set; }
        public string WardType { get; set; }
    }
}