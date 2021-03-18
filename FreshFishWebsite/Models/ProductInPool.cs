namespace FreshFishWebsite.Models
{
    public class ProductInPool
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int PoolId { get; set; }
        public Pool Pool { get; set; }
        public int TotalProductWeight { get; set; }
    }
}
