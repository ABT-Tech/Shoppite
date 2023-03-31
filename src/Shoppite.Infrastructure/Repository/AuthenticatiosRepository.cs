using Shoppite.Core.Entities;
using Shoppite.Core.Repositories;
using Shoppite.Core.ValueObjects;
using Shoppite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Authentication;

namespace Shoppite.Infrastructure.Repository
{
    public class AuthenticatiosRepository : IAuthenticationsRepository
    {
        private EncryptionHelper EncryptPass = new EncryptionHelper();
        public string message = "";

        protected readonly Shoppite_masterContext _MasterContext;
        public AuthenticatiosRepository(Shoppite_masterContext dbContext)
        {
            _MasterContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<Users> GetAuthenticato_Details(string email, string password,int orgid)
        {
            string ps = this.EncryptPass.Encrypt(password);
            //var UserValidate =  _MasterContext.Users.Where(x => x.Email == email && x.Password == ps && x.OrgId == orgid).FirstOrDefault();

            var q = (from user in _MasterContext.Users
                     join userprofile in _MasterContext.UsersProfile on user.Email equals userprofile.UserName 
                     where user.Email == email && user.Password == ps && user.OrgId == orgid && userprofile.Type == "Client"
                     select user).FirstOrDefault();


            return q;
        }

        public async Task<Logo> Get_Logo(int orgid)
        {
            var logo = _MasterContext.Logo.Where(x => x.OrgId == orgid).FirstOrDefault();
            return logo;
        }

        public async Task<Users> RegisterDetail(string userName, string password, string email, int orgId)
        {
            string EnPass = this.EncryptPass.Encrypt(password);
            var check = _MasterContext.Users.Where(x => x.Username == userName || x.Email == email).FirstOrDefault();
            var checkProfile =  _MasterContext.UsersProfile.FirstOrDefault(x => x.UserName == userName); 
           if(check == null)
           {
              Users users = new Users();
              users.Username = userName;
              users.Password = EnPass;
              users.Email = email;
              users.OrgId = orgId;
              users.CreatedDate = DateTime.Now;
              users.Guid = Guid.NewGuid();

              await  _MasterContext.Users.AddAsync(users);

                if(checkProfile == null)
                {
                    UsersProfile usersProfile = new UsersProfile();
                    usersProfile.UserName = userName;
                    usersProfile.Type = "Client";
                    usersProfile.InsertDate = DateTime.Now;
                    usersProfile.OrgId = orgId;
                    usersProfile.ProfileGuid = users.Guid;
                    await _MasterContext.UsersProfile.AddAsync(usersProfile);
                }
                

           }
            await _MasterContext.SaveChangesAsync();
            return check;
        }
    }
}
