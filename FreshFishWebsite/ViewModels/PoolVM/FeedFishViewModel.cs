using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.ViewModels.PoolVM
{
    public class FeedFishViewModel
    {
        public int StorageId { get; set; }
        public int PoolId { get; set; }
        public string FeedName { get; set;}
        public DateTime ExpireFeedDate { get; set; }
        public double Temperature { get; set; }
        public double Nitrogen { get; set; }
        public double WaterLevel { get; set; }
    }
}
