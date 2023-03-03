﻿using Shoppite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface IMyAccountRepository
    {
        Task<List<f_Get_MyAccount_Data>> GetMyAccountDetail(int orgId, int profileid);
        Task UpdateMyAccountDetail(UsersProfile myaccount);
        Task<UsersProfile> GetProfileByProfileId(int ProfileId);
        Task ChangePassword(Users user);
    }
}
