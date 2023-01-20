using Shoppite.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryMasterModel>> GetCategoryList(int orgId);
        Task<CategoryMasterModel> GetBannerImage(int orgId);
        Task<CategoryMasterModel> GetBottomImage(int orgId);
        Task<IEnumerable<CategoryMasterModel>> GetProductList(int orgId);
    }
}
