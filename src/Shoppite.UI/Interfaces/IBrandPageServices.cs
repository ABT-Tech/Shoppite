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
        Task<MainModel> GetCategoryBy_Org(int orgId);
        Task<MainModel> GetnewProduct(int orgid);
        Task<MainModel> Sp_Getcat(int orgid);
        Task<MainModel> Get_Product_By_Cat(int ID);
        Task<MainModel> CategoryMater(int orgid);
        Task<List<F_getproducts_By_BrandIdModel>> GetProductsByBrand(int OrgId,int BrandId);
        Task News_Letter_Submit(int orgid, string email);
    }
}
