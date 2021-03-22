using FreshFishWebsite.Models;

namespace FreshFishWebsite.Interfaces
{
    public interface IProductRepository : ICrud<Product>, IGetterById
    {
    }
}
