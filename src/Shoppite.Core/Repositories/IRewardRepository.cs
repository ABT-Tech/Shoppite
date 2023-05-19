using Shoppite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface IRewardRepository
    {
        Task<IEnumerable<Reward_Point_Log>> GetRewardBalance(int OrgId);
        Task AddRewards(Reward_Point_Log rewards);
    }
}
