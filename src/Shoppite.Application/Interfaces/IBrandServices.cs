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
        public Task<MainModel> GetCategoryBy_Org(int orgid);
        Task<MainModel> _Getproducts_By_NewArrivals(int orgid);
        Task<MainModel> Sp_Getcat(int orgid);
        Task<MainModel> Get_Product_By_Cat(int ID);
        Task<MainModel> CategoryMaster(int orgid);
        Task<List<ProductDetailModel>> Get_Recently_Product(string id, int orgid);
    }
}
