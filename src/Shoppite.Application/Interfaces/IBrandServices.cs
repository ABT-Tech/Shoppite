using Shoppite.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Interfaces
{
    public interface IBrandServices
    {
       public Task<MainModel> GetBrands(int orgid);
        public Task<MainModel> GetNewProducts(int orgid);
        Task<MainModel> _Getproducts_By_NewArrivals(int orgid);
    }
}
