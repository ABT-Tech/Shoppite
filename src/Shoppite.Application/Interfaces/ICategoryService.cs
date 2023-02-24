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
        Task<List<CategoryMasterModel>> GetCategories(int categoryId,int OrgId);
        Task<CategoryMasterModel> DisplayLogo(int orgId);
        Task<List<CategoryMasterModel>> GetHorizontalBanner(int orgId);
        Task<List<f_getproducts_By_CatID_SpecificationNameModel>> GetAllProductByCategory(int CategoryId);
        Task<List<AttributeSetupModel>> GetAllAttributes(int orgID);
        Task<List<f_getproducts_By_CatID_SpecificationNameModel>> GetAllProductByAttribute(int CategoryId,string SpecificationName);
    }
}
