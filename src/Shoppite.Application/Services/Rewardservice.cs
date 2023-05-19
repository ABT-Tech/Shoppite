
using AutoMapper;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Http;
using Shoppite.Application.Interfaces;
using Shoppite.Application.Mapper;
using Shoppite.Application.Models;
using Shoppite.Core.Entities;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Services
{
    public class Rewardservice:IRewardService
    {
        private readonly IRewardRepository _rewardRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _accessor;
        public Rewardservice(IRewardRepository rewardRepository, IMapper mapper, IHttpContextAccessor accessor)
        {
            _rewardRepository = rewardRepository ?? throw new ArgumentNullException(nameof(rewardRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _accessor = accessor;
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
    }
}
