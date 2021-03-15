using FreshFishWebsite.Interfaces.Registering;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<IEnumerable<IdentityError>> RegisterClaim(IRegisterClaim claim)
        {
            return await claim.Register();
        }
    }
}
