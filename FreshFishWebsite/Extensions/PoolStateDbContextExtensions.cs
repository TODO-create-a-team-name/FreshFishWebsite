using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FreshFishWebsite.Extensions
{
    public static class PoolStateDbContextExtensions
    {
        public static IEnumerable<PoolState> GetPoolStatesByPoolId(this DbSet<PoolState> poolStates, int poolId)
        {
            return poolStates.Where(p => p.PoolId == poolId); 
        }
    }
}
