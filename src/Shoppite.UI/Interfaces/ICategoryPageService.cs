﻿

using Shoppite.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoppite.Web.Interfaces
{
    public interface ICategoryPageService
    {
        Task<List<CategoryMasterModel>> GetTopBannerImage(int orgId);
        Task<List<CategoryMasterModel>> GetMiddelBannerImage(int orgId);
        Task<IEnumerable<MainCategoryModel>> GetProductList(int orgId);
        Task<List<CategoryMasterModel>> GetCategories(int CAtegoryId,int OrgId);
        Task<CategoryMasterModel> DisplayLogo(int orgId);
        Task<List<CategoryMasterModel>> GetHorizontalBanner(int orgID);
        Task<List<f_getproducts_By_CatID_SpecificationNameModel>> GetAllProductByCategory(int CategoryId,int OrgId);
        Task<List<f_getproducts_By_CatID_SpecificationNameModel>> GetAllProductByAttribute(int CategoryId,string SpecificationName);
        Task<List<AttributeSetupModel>> GetAllAttributes(int orgId);
        Task<List<CategoryMasterModel>> GetBannerByCategory(int orgId);
        Task<List<sp_getcat_ResultModel>> GetAllCategories(int OrgId);
    }
}
