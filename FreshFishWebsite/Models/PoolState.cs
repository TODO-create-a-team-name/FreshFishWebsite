using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Models
{
    public class PoolState
    {
        public Pool Pool { get; set; }
        public int Id { get; set; }
        public int PoolId { get; set; }
        public double Temperature { get; set; }
        public double Nitrogen { get; set; }
        public double WaterLevel { get; set; }
    }
}
