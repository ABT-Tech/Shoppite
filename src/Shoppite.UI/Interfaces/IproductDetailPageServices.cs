using Shoppite.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Interfaces
{
    public interface IproductDetailPageServices
    {
        Task<ProductDetailModel> GetProductDetails(Guid id, int orgid,string? userName);
        Task<List<ProductDetailModel>> GetproductImages(Guid id);
        Task<ProductDetailModel> GetProductPrice(Guid id);
        Task<ProductDetailModel> Get_Brand_Name(Guid guid, int id);
        Task<ProductDetailModel> ProductAttribute(int AtId);
        Task AddToCart(ProductDetailModel productDetail);
        Task<ProductDetailModel> BuyNow(ProductDetailModel productDetailModel);
        Task<List<f_getproduct_varient_By_GuidModel>> GetProductVarients(Guid guid, int orgid, string userName);

    }
}
