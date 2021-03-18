using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces.Registering
{
    public interface IRegisterClaim
    {
        public Task<IEnumerable<IdentityError>> Register();
    }
}
