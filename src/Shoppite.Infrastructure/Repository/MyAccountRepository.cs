using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shoppite.Core.Entities;
using Shoppite.Core.Repositories;
using Shoppite.Core.ValueObjects;
using Shoppite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.Infrastructure.Repository
{
    public class MyAccountRepository : IMyAccountRepository
    {
        protected readonly Shoppite_masterContext _dbContext;
        private EncryptionHelper EncryptPass = new EncryptionHelper();
        protected readonly IHttpContextAccessor _httpContext;

        public MyAccountRepository(Shoppite_masterContext dbContext, IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<f_Get_MyAccount_Data> GetMyAccountDetail(int orgId,int profileid)
        {
            string sql = "select * from f_Get_MyAccount_Data(@orgid,@profileid)";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@orgid", Value = orgId },
                 new SqlParameter { ParameterName = "@profileid", Value = profileid }
            };
            return await _dbContext.Set<f_Get_MyAccount_Data>().FromSqlRaw(sql, parms.ToArray()).FirstOrDefaultAsync();
        }
        public async Task UpdateMyAccountDetail(UsersProfile myaccount)
        {
            UsersProfile usersProfile = await _dbContext.UsersProfile.FindAsync(myaccount.ProfileId);
            //usersProfile.UserName = myaccount.UserName;
            usersProfile.CoverImage = myaccount.CoverImage;
            usersProfile.City = myaccount.City;
            usersProfile.State = myaccount.State;
            usersProfile.ContactNumber = myaccount.ContactNumber;
            usersProfile.Address = myaccount.Address;
            usersProfile.City = myaccount.City;
            usersProfile.State = myaccount.State;
            usersProfile.Country = myaccount.Country;

                if (usersProfile != null)
                {
                    _dbContext.Entry(usersProfile).State = EntityState.Detached;
                }
                _dbContext.Entry(usersProfile).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();           
        }
        public async Task<UsersProfile> GetProfileByProfileId(int Profileid)
        {
            var Details = await _dbContext.UsersProfile.Where(x => x.ProfileId == Profileid).FirstOrDefaultAsync();
            return Details;
        }
        public async Task ChangePassword(Users users)
        {
            Users user = await _dbContext.Users.FirstOrDefaultAsync(X=>X.Email == users.Username);

            //usersProfile.UserName = myaccount.UserName;
            string newPass = EncryptPass.Encrypt(users.Password);
            user.Password = newPass;

            if (user != null)
            {
                _dbContext.Entry(user).State = EntityState.Detached;
            }
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GetProfileId(string username, int orgid)
        {
            var findUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == username && x.OrgId == orgid);
            var ProfileFind = await _dbContext.UsersProfile.FirstOrDefaultAsync(x => x.UserName == findUser.Email && x.OrgId == orgid);
            return ProfileFind.ProfileId;
        }
        public async Task AddAddressDetails(UserAddress myaccount)
        {
            if (myaccount.Id == 0)
            {
                var UserName = _httpContext.HttpContext.User.Identity.Name;
                var UserId = _dbContext.Users.FirstOrDefault(u => u.Email == UserName && u.OrgId == myaccount.OrgId);
                UserAddress accountDetails = new UserAddress();
                accountDetails.FirstName = myaccount.FirstName;
                accountDetails.LastName = myaccount.LastName;
                accountDetails.Landmark = myaccount.Landmark;
                accountDetails.Address = myaccount.Address;
                accountDetails.Address1 = myaccount.Address1;
                accountDetails.Mobile = myaccount.Mobile;
                accountDetails.State = myaccount.State;
                accountDetails.AltMobile = myaccount.AltMobile;
                accountDetails.City = myaccount.City;
                accountDetails.Email = myaccount.Email;
                accountDetails.AddressType = myaccount.AddressType;
                accountDetails.IsDefault = myaccount.IsDefault;
                accountDetails.Pincode = myaccount.Pincode;
                accountDetails.OrgId = myaccount.OrgId;
                accountDetails.UserId = UserId.UserId;

                _dbContext.UserAddress.Add(accountDetails);
            }
            else
            {
                UserAddress details = await _dbContext.UserAddress.FindAsync(myaccount.Id);
                details.FirstName = myaccount.FirstName;
                details.LastName = myaccount.LastName;
                details.Landmark = myaccount.Landmark;
                details.Address = myaccount.Address;
                details.Address1 = myaccount.Address1;
                details.Mobile = myaccount.Mobile;
                details.State = myaccount.State;
                details.AltMobile = myaccount.AltMobile;
                details.City = myaccount.City;
                details.Email = myaccount.Email;
                details.AddressType = myaccount.AddressType;
                details.IsDefault = myaccount.IsDefault;
                details.Pincode = myaccount.Pincode;

                if (myaccount != null)
                {

                    _dbContext.Entry(details).State = EntityState.Detached;
                }
                _dbContext.Entry(details).State = EntityState.Modified;
            }
            await  _dbContext.SaveChangesAsync();
        }
        public async Task<List<UserAddress>> GetAddressDetail(int orgId)
        {
            var UserName = _httpContext.HttpContext.User.Identity.Name;
            var Id = _dbContext.Users.FirstOrDefault(u => u.Email == UserName);
            var q = (from myaccount in _dbContext.UserAddress
                     join userdetail in _dbContext.Users on myaccount.UserId equals userdetail.UserId                    
                     where myaccount.UserId==Id.UserId
                     select myaccount).ToList();
            return q;

        }

        public async Task<UserAddress> GetAddressdetailBYId(int orgId, int Id)
        {
            var UserName = _httpContext.HttpContext.User.Identity.Name;
            var GetUserId = _dbContext.Users.FirstOrDefault(u => u.Email == UserName && u.OrgId == orgId);
            var q = (from myaccount in _dbContext.UserAddress
                     join userdetail in _dbContext.Users on myaccount.UserId equals userdetail.UserId
                     where myaccount.OrgId == orgId && myaccount.UserId == GetUserId.UserId && myaccount.Id==Id
                     select myaccount).FirstOrDefault();
            return q;
        }
        public async Task DeleteAddressDetail(int orgId, int Id)
        {
            var deltecategory = await _dbContext.UserAddress.FindAsync(Id);

            if (deltecategory != null)
            {
                _dbContext.UserAddress.Remove(deltecategory);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
