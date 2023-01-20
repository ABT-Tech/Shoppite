

using Shoppite.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoppite.Web.Interfaces
{
    public interface ICategoryPageService
    {
        Task<IEnumerable<CategoryMasterModel>> GetCategoryList(int orgId);
        Task<CategoryMasterModel> GetBannerImage(int orgId);
        Task<CategoryMasterModel> GetBottomImage(int orgId);
        Task<IEnumerable<CategoryMasterModel>> GetProductList(int orgId);
    }
}
