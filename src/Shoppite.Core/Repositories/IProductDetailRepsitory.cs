using Shoppite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
   public interface IProductDetailRepsitory
   {
        Task<ProductBasic> productBasic(Guid id);
        Task<List<ProductImages>> ProductImages(Guid id);
        Task<ProductPrice> productPrices(Guid id);
        Task<Brands> Get_Brand_Name(Guid guid, int id);
        Task<ProductCategory> Get_Product_Cat(Guid guid);
        Task<List<f_getproducts_By_CategoryID>> F_Getproducts_By_CategoryID(int id);
        Task<List<f_getproducts_Recentlyviewed>> F_Getproducts_Recentlyviewed(string id, int orgid);
        Task<ProductRecentlyViewed> GetIP(int orgid);
        Task AddIp(ProductRecentlyViewed productRecentlyViewed);
   }
}
