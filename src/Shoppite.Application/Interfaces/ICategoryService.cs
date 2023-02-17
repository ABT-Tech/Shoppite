using Shoppite.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoppite.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryMasterModel>> GetTopBannerImage(int orgId);
        Task<List<CategoryMasterModel>> GetMiddelBannerImage(int orgId);
        Task<IEnumerable<MainCategoryModel>> GetProductList(int orgId);
        Task<List<CategoryMasterModel>> GetCategories(int categoryId);
        Task<CategoryMasterModel> DisplayLogo(int orgId);
        Task<List<CategoryMasterModel>> GetHorizontalBanner(int orgId);
        Task<List<f_getproducts_By_CatID_SpecificationNameModel>> GetAllProductByCategory(int CategoryId);
        Task<List<CategoryMasterModel>> GetAllSubCategories(int MainCategoryId);
        Task<List<AttributeSetupModel>> GetAllAttributes(int orgID);
        Task<List<f_getproducts_By_CatID_SpecificationNameModel>> GetAllProductByAttribute(int CategoryId,string SpecificationName);
        Task<List<Customer_WishlistModel>> GetWishList(string Username,int OrgId);
        Task AddWishList(MainModel wishlist, int ProductId);
    }
}
