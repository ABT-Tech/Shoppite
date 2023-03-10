﻿using AutoMapper;
using AutoMapper.Internal.Mappers;
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
    public class MyAccountService:IMyAccountService
    {
        private readonly IMyAccountRepository _myAccountRepository;
        private readonly IMapper _mapper;
        public MyAccountService(IMyAccountRepository myAccountRepository, IMapper mapper)
        {
            _myAccountRepository = myAccountRepository ?? throw new ArgumentNullException(nameof(myAccountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<f_Get_MyAccount_Data_Model>> GetMyAccountDetail(int orgId,int profileid)
        {
            var myAccount_Data = await _myAccountRepository.GetMyAccountDetail(orgId,profileid);
            var mapped = ObjectMapper.Mapper.Map<List<f_Get_MyAccount_Data_Model>>(myAccount_Data);
            return mapped;
        }
        public async Task UpdateMyAccountDetail(MainModel myaccount)
        {
            var mapped = ObjectMapper.Mapper.Map<UsersProfile>(myaccount);
            await _myAccountRepository.UpdateMyAccountDetail(mapped);

        }
        public async Task<MainModel> GetProfileByProfileId(int OrgId)
        {
            var Details = await _myAccountRepository.GetProfileByProfileId(OrgId);
            var mapped = ObjectMapper.Mapper.Map<MainModel>(Details);
            return mapped;
        }
        public async Task ChangePassword(MainModel model)
        {
            var mapped = ObjectMapper.Mapper.Map<Users>(model);
            await _myAccountRepository.ChangePassword(mapped);
        }

    }
}
