namespace FreshFishWebsite.Interfaces
{
    public interface IRepository<T> : ICrud<T>
    {
        T GetById(int? id);
    }
}
