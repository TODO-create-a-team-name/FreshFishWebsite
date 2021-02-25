using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Models
{
    [Table("Drivers")]
    public class Driver : User
    {
        public int OrderItemsId { get; set; }
        public List<OrderItems> OrderItems { get; set; } = new();
        public int StorageId { get; set; }
        public Storage Storage { get; set; }
        public bool IsDelivering { get; set; } = false;
    }
}
