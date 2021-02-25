

using FreshFishWebsite.Models;

namespace FreshFishWebsite.ViewModels
{
    public class OrderDetailsForDriverViewModel
    {
        public int OrderItemsId { get; set; }
        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public OrderStatus Status { get; set; }
    }
}
