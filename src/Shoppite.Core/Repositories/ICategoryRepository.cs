

using Shoppite.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryMaster>> GetCategoryList(int orgId);
        Task<AdsDetail> GetBannerImage(int orgId);
        Task<AdsDetail> GetBottomImage(int orgId);
        Task<IEnumerable<ProductBasic>> GetProductList(int orgId);
    }
}
