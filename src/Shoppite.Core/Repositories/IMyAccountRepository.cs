using Shoppite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface IMyAccountRepository
    {
        Task<f_Get_MyAccount_Data> GetMyAccountDetail(int profileid);
        Task UpdateMyAccountDetail(UsersProfile myaccount);
        Task<UsersProfile> GetProfileByProfileId(int ProfileId);
        Task ChangePassword(Users user);
        Task<int> GetProfileId(string username,int orgid);
        Task AddAddressDetails(UserAddress myaccount);
        Task<List<UserAddress>> GetAddressDetail(int orgId);
        Task<UserAddress> GetAddressdetailBYId(int orgId,int Id);
        Task DeleteAddressDetail(int orgId, int Id);
        Task UpdateCoverImage(UsersProfile myaccount);
        Task<List<Coupon>> GetCouponDetails();
        Task<Coupon> GetCouponDetailsByCouponId(int CouponId);
    }
}
