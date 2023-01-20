using Shoppite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
   public interface IBrandRepository
    {
        Task<List<Brands>> GetBrands(int orgid);
        Task<IQueryable<f_getproducts_By_NewArrivals>> GetNewProducts(int orgid);
        Task<List<f_getproducts_By_NewArrivals>> _Getproducts_By_NewArrivals(int orgid);
    }
}
