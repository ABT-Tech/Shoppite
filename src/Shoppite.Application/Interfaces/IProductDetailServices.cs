using Shoppite.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Interfaces
{
   public interface IProductDetailServices
    {
        Task<ProductDetailModel> GetProductDetails(Guid id,int orgid,string? Username);
        Task<List<ProductDetailModel>> GetProductImages(Guid id);
        Task<ProductDetailModel> GetProductPrice(Guid id);
        Task<ProductDetailModel> Get_Brand_Name(Guid guid, int id);
        Task<ProductDetailModel> Get_Product_Cat(Guid guid);
        Task<List<ProductDetailModel>> Get_Similar_Product(int id);
        Task<ProductDetailModel> GetIP(int orgid);
        Task AddIp(ProductDetailModel detailModel);
        Task<List<ProductDetailModel>> Get_Recently_Product(string id, int orgid);
        Task<ProductDetailModel> ProductAttribute(int AtId);
        Task AddToCart(ProductDetailModel productDetailModel);
        Task AddOrderMaster(ProductDetailModel productDetailModel);
        Task<ProductDetailModel> BuyNow(ProductDetailModel mapped);
        Task<List<f_getproduct_varient_By_GuidModel>> GetProductVarients(Guid guid, int orgid, string userName);
    }
}
