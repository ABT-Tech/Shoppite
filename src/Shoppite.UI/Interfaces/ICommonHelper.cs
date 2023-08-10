﻿using Microsoft.AspNetCore.Http;
using Shoppite.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Interfaces
{
   public interface ICommonHelper
    {
        int GetOrgID(HttpContext httpContext);
        bool DoesPropertyExist(dynamic settings, string name);
        Core.Entities.OrganizationAggregatorControl GetMerchantDetails(HttpContext httpContext);
        string DecryptAES256_V3(string cipherText, string _key, string _iv);
        string EncryptAES256_V3(string plainText, string _key, string _iv);
        string OrganizationName(HttpContext httpContext);
        Core.Entities.Organization OrganizationDetails(int orgID);
        void LogError(string msg);
    }
}
