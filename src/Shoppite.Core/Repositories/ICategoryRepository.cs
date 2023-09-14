﻿

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
        Task<List<CategoryMaster>> GetCategories();
        Task<Logo> DisplayLogo(int orgId);
        Task<List<AdsDetail>> GetBottomBanner(int orgID);
        Task<List<F_getproducts_By_CatId>> GetAllProductByCategory(string CategoryId);
        Task<List<SP_GetSpecificationData_AttributName>> GetAllAttributes(int orgId);
        Task<List<f_getproducts_By_CatID_SpecificationName>> GetAllProductByAttribute(string CategoryId, string AttributeName);
        Task<List<CategoryMaster>> GetBannerByCategory(int orgId);
        Task<List<sp_getcat_Result>> GetAllCategories(int OrgId);
        Task<CategoryMaster> MaincatDetails(int? catId, string catname);
        Task<CategoryMaster> FindChaildCat(int? childCatId, string childCatname, int? catId);
        Task<List<AdsDetail>> GetCategoryBannerImage(int orgId);
        Task<List<AdsDetail>> GetLeftBanner(int orgId);
        Task<List<SP_GetSimilarProducts>> GetSimilarProducts(string CategoryId, int BrandId, int OrgId);
        Task<List<F_getproducts_By_CatId>> GetProductsByCategoryInProductList(int CategoryId,int Orgid);
        Task<List<SP_GetCategoryWiseProductCount>> GetProductCount(int OrgId);

    }
}
