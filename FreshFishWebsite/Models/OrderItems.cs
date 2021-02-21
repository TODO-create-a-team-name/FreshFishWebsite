namespace FreshFishWebsite.Models
{
    public enum OrderStatus
    {
        Waiting,
        OnWay,
        Delivered
    }
    public class OrderItems
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int StorageId { get; set; }

        public Storage Storage { get; set; }

        public string DriverId { get; set; }

        public Driver Driver { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Waiting;

    }
}
