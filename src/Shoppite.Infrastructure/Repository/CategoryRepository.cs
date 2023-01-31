
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shoppite.Application.Models;
using Shoppite.Core.Entities;
using Shoppite.Core.Repositories;
using Shoppite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly Shoppite_masterContext _dbContext;
        public CategoryRepository(Shoppite_masterContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<AdsDetail>> GetTopBannerImage(int orgId)
        {
  
            var q = (from ad_detail in _dbContext.AdsDetail
                     join ad_place in _dbContext.AdsPlacement on ad_detail.AdsPlacementId equals ad_place.AdsPlacementId
                     join ad_pagename in _dbContext.AdsPageName on ad_detail.AdsPageId equals ad_pagename.AdsPageId
                     where ad_pagename.PageName=="Home" && ad_place.PlacementName == "Top" && ad_detail.Status == "Active" && ad_detail.OrgId == orgId
                     select ad_detail).ToList();
            return q;
        }
        public async Task<List<AdsDetail>> GetMiddelBannerImage(int orgId)
        {
            var q = (from ad_detail in _dbContext.AdsDetail
                     join ad_place in _dbContext.AdsPlacement on ad_detail.AdsPlacementId equals ad_place.AdsPlacementId
                     join ad_pagename in _dbContext.AdsPageName on ad_detail.AdsPageId equals ad_pagename.AdsPageId
                     where ad_pagename.PageName.Contains("Home") && ad_place.PlacementName == "Middle" && ad_detail.OrgId == orgId
                     select ad_detail).ToList();
            return q;
        }
        public async Task<IEnumerable<f_getproducts_By_CategoryID_Result>> GetProductList(int orgId)
        {
            string sql = "select * from f_getproducts_By_OrgID(@ID)";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@ID", Value = orgId }
            };
             return await _dbContext.Set<f_getproducts_By_CategoryID_Result>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
        }
        public async Task<List<CategoryMaster>> GetCategories(int CategoryId)
        {
            return await _dbContext.CategoryMaster.ToListAsync();
        }
        public async Task<Logo> DisplayLogo(int orgId)
        {
            return await _dbContext.Logo.Where(x => x.OrgId == orgId).FirstOrDefaultAsync();
        }
        public async Task<List<AdsDetail>> GetHorizontalBanner(int orgId)
        {
            var q = (from ad_detail in _dbContext.AdsDetail
                     join ad_place in _dbContext.AdsPlacement on ad_detail.AdsPlacementId equals ad_place.AdsPlacementId
                     join ad_pagename in _dbContext.AdsPageName on ad_detail.AdsPageId equals ad_pagename.AdsPageId
                     where ad_pagename.PageName.Contains("Home") && ad_place.PlacementName == "Horizontal" && ad_detail.OrgId == orgId
                     select ad_detail).ToList();
            return q;
        }

    }
}
