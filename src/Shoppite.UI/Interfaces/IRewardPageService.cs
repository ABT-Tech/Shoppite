using Shoppite.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Interfaces
{
    public interface IRewardPageService
    {
        Task<List<Reward_Point_LogModel>> GetRewardBalance(int OrgId);
        Task AddRewards(Reward_Point_LogModel rewards);
    }
}
