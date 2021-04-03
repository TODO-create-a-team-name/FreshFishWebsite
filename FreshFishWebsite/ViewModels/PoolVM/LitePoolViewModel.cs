using FreshFishWebsite.Models;
using System.Collections.Generic;

namespace FreshFishWebsite.ViewModels
{
    public class LitePoolViewModel
    {
        public int StorageId { get; set; }

        public IEnumerable<Pool> Pools { get; set; }
    }
}
