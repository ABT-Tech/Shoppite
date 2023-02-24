

using Shoppite.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<AdsDetail>> GetTopBannerImage(int orgId);
        Task<List<AdsDetail>> GetMiddelBannerImage(int orgId);
        Task<IEnumerable<f_getproducts_By_CategoryID_Result>> GetProductList(int orgId);
        Task<List<CategoryMaster>> GetCategories(int CategoryId, int OrgId);
        Task<Logo> DisplayLogo(int orgId);
        Task<List<AdsDetail>> GetHorizontalBanner(int orgID);
        Task<List<f_getproducts_By_CategoryID>> GetAllProductByCategory(int CategoryId);
        Task<List<SP_GetSpecificationData_AttributName>> GetAllAttributes(int orgId);
        Task<List<f_getproducts_By_CatID_SpecificationName>> GetAllProductByAttribute(int CategoryId, string AttributeName);

    }
}
