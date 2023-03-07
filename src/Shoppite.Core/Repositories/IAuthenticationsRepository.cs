using Shoppite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
   public interface IAuthenticationsRepository
    {   
        Task<Users> GetAuthenticato_Details(string username, string password,int orgid);
        Task<Logo> Get_Logo(int orgid);
    }
}
    