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
    public class MyAccountService:IMyAccountService
    {
        private readonly IMyAccountRepository _myAccountRepository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _accessor;
        public MyAccountService(IMyAccountRepository myAccountRepository, IMapper mapper, IHttpContextAccessor accessor)
        {
            _myAccountRepository = myAccountRepository ?? throw new ArgumentNullException(nameof(myAccountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _accessor = accessor;
        }
        public async Task<f_Get_MyAccount_Data_Model> GetMyAccountDetail(int orgId,int profileid)
        {
            var myAccount_Data = await _myAccountRepository.GetMyAccountDetail(orgId,profileid);
            var mapped = ObjectMapper.Mapper.Map<f_Get_MyAccount_Data_Model>(myAccount_Data);
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
            //model.Username = _accessor.HttpContext.User.Identity.Name;
            model.UserName = _accessor.HttpContext.User.Identity.Name;
            var mapped = ObjectMapper.Mapper.Map<Users>(model);
            await _myAccountRepository.ChangePassword(mapped);
        }

        public async Task<int> GetProfileId(string username,int orgid)
        {
            return await _myAccountRepository.GetProfileId(username,orgid);
        }
        public async Task AddAddressDetails(MyAccountDetailsModel myaccount)
        {
            var mapped = ObjectMapper.Mapper.Map<UserAddress>(myaccount);
            await _myAccountRepository.AddAddressDetails(mapped);
        }
        public async Task<List<MyAccountDetailsModel>> GetAddressDetail(int orgId)
        {
            var myAccount_Data = await _myAccountRepository.GetAddressDetail(orgId);
            var mapped = ObjectMapper.Mapper.Map<List<MyAccountDetailsModel>>(myAccount_Data);
            return mapped;
        }
        public async Task<MyAccountDetailsModel> GetAddressdetailBYId(int orgId, int Id)
        {
            var myAccount_Data = await _myAccountRepository.GetAddressdetailBYId(orgId,Id);
            var mapped = ObjectMapper.Mapper.Map<MyAccountDetailsModel>(myAccount_Data);
            return mapped;
        }
        public async Task DeleteAddressDetail(int orgId, int Id)
        {
            await _myAccountRepository.DeleteAddressDetail(orgId, Id);
        }
    }
}
