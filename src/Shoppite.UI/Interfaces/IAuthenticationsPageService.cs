using Shoppite.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Interfaces
{
   public interface IAuthenticationsPageService
    {
        Task<UsersModal> Get_Login_Data(string username,string password, int orgid);
        Task<UsersModal> Get_Logo(int orgid);
    }
}
