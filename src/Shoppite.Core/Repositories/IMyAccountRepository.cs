﻿using Shoppite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface IMyAccountRepository
    {
        Task<f_Get_MyAccount_Data> GetMyAccountDetail(int orgId, int profileid);
        Task UpdateMyAccountDetail(UsersProfile myaccount);
        Task<UsersProfile> GetProfileByProfileId(int ProfileId);
        Task ChangePassword(Users user);
        Task<int> GetProfileId(string username,int orgid);
        Task AddAddressDetails(MyAccountDetails myaccount);
        Task<List<MyAccountDetails>> GetAddressDetail(int orgId);
        Task<MyAccountDetails> GetAddressdetailBYId(int orgId,int Id);
        Task DeleteAddressDetail(int orgId, int Id);
    }
}
