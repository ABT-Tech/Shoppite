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
        public async Task<Users> GetAuthenticato_Details(string username, string password,int orgid)
        {
            string ps = this.EncryptPass.Encrypt(password);
            var UserValidate =  _MasterContext.Users.Where(x => x.Username == username && x.Password == ps && x.OrgId == orgid).FirstOrDefault();
            return UserValidate;
        }

        public async Task<Logo> Get_Logo(int orgid)
        {
            var logo = _MasterContext.Logo.Where(x => x.OrgId == orgid).FirstOrDefault();
            return logo;
        }
    }
}
