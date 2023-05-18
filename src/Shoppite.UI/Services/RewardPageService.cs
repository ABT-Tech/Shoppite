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
        private readonly IMapper _mapper;
        public RewardPageService(IRewardService rewardService, IMapper mapper)
        {
            _RewardService = rewardService ?? throw new ArgumentNullException(nameof(rewardService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<Reward_Point_LogModel>> GetRewardBalance(int OrgId, int ProfileId)
        {
            var rewards = await _RewardService.GetRewardBalance(OrgId, ProfileId);
            return rewards;
        }
    }
}
