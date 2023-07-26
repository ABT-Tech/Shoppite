﻿using Shoppite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
   public interface ICartRepository
    {
        Task<List<f_getproduct_CartDetails_By_Orgid>> OrderBasic(int orgid);
        Task<OrderBasic> DeleteAsync(int id);
        Task SaveAddress(OrderShipping orderShipping);
        Task<OrderBasic> CheckOrder(OrderBasic orderBasic);
        Task UpdateOrder(OrderBasic orderBasic);
        Task UpdateOrderQty(OrderBasic orderBasic);
        Task<UsersProfile> FindAddress(string userName);
        Task<UsersProfile> GetVendorDetails(UsersProfile usersProfile);
        Task<OrderShipping> GetAddredd(string userName);
        Task<f_getproduct_CartDetails_By_Orgid> CheckProdInCart(int orgId,string ProductName,string Username,int SpecId);
    }
}
