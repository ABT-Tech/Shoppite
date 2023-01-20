using Shoppite.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Interfaces
{
   public interface IBrandPageServices
    {
        Task<MainModel> GetBrands(int orgId);
        Task<MainModel> GetNewProduct(int orgId);
        Task<MainModel> GetnewProduct(int orgid);
    }
}
