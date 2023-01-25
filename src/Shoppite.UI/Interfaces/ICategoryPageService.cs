

using Shoppite.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoppite.Web.Interfaces
{
    public interface ICategoryPageService
    {
        Task<List<CategoryMasterModel>> GetTopBannerImage(int orgId);
        Task<List<CategoryMasterModel>> GetMiddelBannerImage(int orgId);
        Task<IEnumerable<CategoryProductModel>> GetProductList(int orgId);
        Task<List<CategoryMasterModel>> GetCategories(int CAtegoryId);
        Task<CategoryMasterModel> DisplayLogo(int orgId);
        Task<List<CategoryMasterModel>> GetHorizontalBanner(int orgID);
    }
}
