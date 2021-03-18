using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces.Registering
{
    public interface IAccountRepository
    {
        public Task<IEnumerable<IdentityError>> RegisterClaim(IRegisterClaim claim);
    }
}
