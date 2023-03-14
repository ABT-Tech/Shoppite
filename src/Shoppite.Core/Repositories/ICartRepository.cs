using Shoppite.Core.Entities;
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
        Task<OrderShipping> FindAddress(string userName);
        Task<UsersProfile> GetVendorDetails(UsersProfile usersProfile);
    }
}
