
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Shoppite.Application.Interfaces;
using Shoppite.Application.Mapper;
using Shoppite.Application.Models;
using Shoppite.Core.Entities;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoppite.Application.Services
{
    public class Rewardservice:IRewardService
    {
        private readonly IRewardRepository _rewardRepository;
        public Rewardservice(IRewardRepository rewardRepository, IMapper mapper, IHttpContextAccessor accessor)
        {
            _rewardRepository = rewardRepository ?? throw new ArgumentNullException(nameof(rewardRepository));
        }
        public async Task<List<Reward_Point_LogModel>> GetRewardBalance(int OrgId)
        {
            var reward_Points = await _rewardRepository.GetRewardBalance(OrgId);
            var mapped = ObjectMapper.Mapper.Map<List<Reward_Point_LogModel>>(reward_Points);
            return mapped;
        }
        public async Task AddRewards(Reward_Point_LogModel rewards)
        {
            var mapped = ObjectMapper.Mapper.Map<Reward_Point_Log>(rewards);
            await _rewardRepository.AddRewards(mapped);
        }
        public async Task ClaimReward(Reward_Point_LogModel rewards)
        {
            var mapped = ObjectMapper.Mapper.Map<Reward_Point_Log>(rewards);
            await _rewardRepository.ClaimReward(mapped);
        }
    }
}
