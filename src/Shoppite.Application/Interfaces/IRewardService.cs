using Shoppite.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Interfaces
{
    public interface IRewardService
    {
        Task<List<Reward_Point_LogModel>> GetRewardBalance(int OrgId);
        Task AddRewards(Reward_Point_LogModel reward);
        Task ClaimReward(Reward_Point_LogModel reward);
    }
}
