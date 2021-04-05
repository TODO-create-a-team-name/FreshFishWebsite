using System;

namespace FreshFishWebsite.Models
{
    public class Feeding
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTimeFed { get; set; }
        public DateTime DateTimeFeedingExpired { get; set; }
        public int PoolId { get; set; }
        public Pool Pool { get; set; }
    }
}
