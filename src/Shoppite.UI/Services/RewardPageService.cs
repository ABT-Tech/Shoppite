using AutoMapper;
using Shoppite.Application.Interfaces;
using Shoppite.Application.Models;
using Shoppite.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Services
{
    public class RewardPageService :IRewardPageService
    {
        private readonly IRewardService _RewardService;
       
        public RewardPageService(IRewardService rewardService)
        {
            _RewardService = rewardService ?? throw new ArgumentNullException(nameof(rewardService));         
        }
        public async Task<List<Reward_Point_LogModel>> GetRewardBalance(int OrgId)
        {
            var rewards = await _RewardService.GetRewardBalance(OrgId);
            return rewards;
        }
        public async Task AddRewards(Reward_Point_LogModel mainModel)
        {
            await _RewardService.AddRewards(mainModel);
        }
        public async Task ClaimReward(Reward_Point_LogModel mainModel)
        {
            await _RewardService.ClaimReward(mainModel);
        }
    }
}
