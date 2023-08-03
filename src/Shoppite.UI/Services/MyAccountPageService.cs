﻿using AutoMapper;
using Shoppite.Application.Interfaces;
using Shoppite.Application.Models;
using Shoppite.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoppite.UI.Services
{
    public class MyAccountPageService:IMyAccountPageService
    {
        private readonly IMyAccountService _myAccountService;
        private readonly IMapper _mapper;
        public MyAccountPageService(IMyAccountService myAccountService, IMapper mapper)
        {
            _myAccountService = myAccountService ?? throw new ArgumentNullException(nameof(myAccountService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<f_Get_MyAccount_Data_Model> GetMyAccountDetail(int orgId,int profileid)
        {
            var myaccount = await _myAccountService.GetMyAccountDetail(orgId, profileid);
            return myaccount;
        }
        public async Task UpdateMyAccountDetail(MainModel myaccount)
        {
            await _myAccountService.UpdateMyAccountDetail(myaccount);
        }
        public async Task<MainModel> GetProfileByProfileId(int profileid)
        {
            return await _myAccountService.GetProfileByProfileId(profileid);
        }
        public async Task ChangePassword(MainModel model)
        {
             await _myAccountService.ChangePassword(model);
        }

        public async Task<int> GetProfileId(string username,int orgid)
        {
            return await _myAccountService.GetProfileId(username, orgid);
        }
        public async Task AddAddressDetails(MyAccountDetailsModel myaccount)
        {
            await _myAccountService.AddAddressDetails(myaccount);
        }
        public async Task<List<MyAccountDetailsModel>> GetAddressDetail(int orgId)
        {
            var myaccount = await _myAccountService.GetAddressDetail(orgId);
            return myaccount;
        }
        public async Task<MyAccountDetailsModel> GetAddressdetailBYId(int orgId,int Id)
        {
            var addressdetail = await _myAccountService.GetAddressdetailBYId(orgId,Id);
            return addressdetail;
        }
        public async Task DeleteAddressDetail(int orgId, int Id)
        {
            await _myAccountService.DeleteAddressDetail(orgId, Id);
        }
    }
}
