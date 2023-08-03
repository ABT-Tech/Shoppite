﻿using Shoppite.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Interfaces
{
   public  interface IMyAccountService
    {
        Task<f_Get_MyAccount_Data_Model> GetMyAccountDetail(int orgId,int profileid);
        Task UpdateMyAccountDetail(MainModel myaccount);
        Task<MainModel> GetProfileByProfileId(int profileid);
        Task ChangePassword(MainModel model);
        Task<int> GetProfileId(string username,int orgid);
        Task AddAddressDetails(MyAccountDetailsModel myaccount);
        Task<List<MyAccountDetailsModel>> GetAddressDetail(int orgId);
        Task<MyAccountDetailsModel> GetAddressdetailBYId(int orgId ,int Id);
        Task DeleteAddressDetail(int orgId, int Id);
    }
}
