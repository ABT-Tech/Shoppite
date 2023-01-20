
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
        public async Task<IEnumerable<CategoryMaster>> GetCategoryList(int orgId)
        {
            //var list = await _dbContext.CategoryMaster.ToListAsync();
            var q = from c in _dbContext.CategoryMaster
                    where c.OrgId==orgId && c.IsPublished==true && c.IsShowHomePage==true &&  c.ParentCategoryId == 0
                    select c;
            return q;
     
        }
        public async Task<AdsDetail> GetBannerImage(int orgId)
        {
            /*var q = (from a in _dbContext.AdsDetail
                    where a.OrgId == orgId
                    select a).FirstOrDefault();
           *//* var list = await _dbContext.AdsDetail.ToListAsync();*//*
            return q;*/
          //  string page = this.Page.Title.ToString();
   
            var q = (from ad_detail in _dbContext.AdsDetail
                     join ad_place in _dbContext.AdsPlacement on ad_detail.AdsPlacementId equals ad_place.AdsPlacementId
                     join ad_pagename in _dbContext.AdsPageName on ad_detail.AdsPageId equals ad_pagename.AdsPageId
                     where ad_pagename.PageName.Contains("Home") && ad_place.PlacementName == "Top" && ad_detail.Status == "Active" && ad_detail.OrgId == orgId
                     select ad_detail).FirstOrDefault();
            return q;
        }
        public async Task<AdsDetail> GetBottomImage(int orgId)
        {
            /*var q = (from a in _dbContext.AdsDetail
                     where a.OrgId == orgId
                     select a).FirstOrDefault();
            return q;
        }*/
            var q = (from ad_detail in _dbContext.AdsDetail
                     join ad_place in _dbContext.AdsPlacement on ad_detail.AdsPlacementId equals ad_place.AdsPlacementId
                     join ad_pagename in _dbContext.AdsPageName on ad_detail.AdsPageId equals ad_pagename.AdsPageId
                     where ad_pagename.PageName.Contains("Home") && ad_place.PlacementName == "Bottom" && ad_detail.OrgId == orgId
                     select ad_detail).FirstOrDefault();
            return q;
        }
        public async Task<IEnumerable<ProductBasic>> GetProductList(int orgId)
        {
            //var list = await _dbContext.CategoryMaster.ToListAsync();
            var q = from c in _dbContext.ProductBasic
                    where c.OrgId == orgId && c.IsPublished==true
                    select c;
            return q;

        }
    }
}
