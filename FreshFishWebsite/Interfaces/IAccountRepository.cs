using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IAccountRepository<T>
    {
        public Task<IEnumerable<IdentityError>> RegisterUser(RegisterStorageWorker model);
    }
}
