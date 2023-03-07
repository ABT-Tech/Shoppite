using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shoppite.Core.Entities;
using Shoppite.Core.Repositories;
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

        public MyAccountRepository(Shoppite_masterContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<f_Get_MyAccount_Data>> GetMyAccountDetail(int orgId,int profileid)
        {
            string sql = "select * from f_Get_MyAccount_Data(@orgid,@profileid)";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@orgid", Value = orgId },
                 new SqlParameter { ParameterName = "@profileid", Value = profileid }
            };
            return await _dbContext.Set<f_Get_MyAccount_Data>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
        }
        public async Task UpdateMyAccountDetail(UsersProfile myaccount)
        {
            UsersProfile usersProfile = await _dbContext.UsersProfile.FindAsync(myaccount.ProfileId);
            //usersProfile.UserName = myaccount.UserName;
            usersProfile.CoverImage = myaccount.CoverImage;
            usersProfile.ContactNumber = myaccount.ContactNumber;
            usersProfile.Address = myaccount.Address;

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
            Users user = await _dbContext.Users.FindAsync(users.UserId);
            //usersProfile.UserName = myaccount.UserName;
            user.Password = users.Password;

            if (user != null)
            {
                _dbContext.Entry(user).State = EntityState.Detached;
            }
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
