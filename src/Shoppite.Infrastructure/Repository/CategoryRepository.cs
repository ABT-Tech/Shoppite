
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
            //string strConnection = "Server=DESKTOP-23U72N4;Database=Shoppite_master;Trusted_Connection=True;";
            //string ExecuteFunction = "select * from f_getproducts_By_OrgID(@ID)";
            //List<f_getproducts_By_CategoryID_Result> f_getproducts_By_CategoryID_ResultList = new List<f_getproducts_By_CategoryID_Result>(); 
            //using (SqlConnection connection = new SqlConnection(strConnection))
            //{
            //    SqlCommand command = new SqlCommand(ExecuteFunction, connection);
            //    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = orgId;
            //    connection.Open();
            //    SqlDataReader rdr = command.ExecuteReader();

            //    while (rdr.Read())
            //    {
            //        f_getproducts_By_CategoryID_Result f_getproducts_By_CategoryID_ResultObj = new f_getproducts_By_CategoryID_Result();
            //        f_getproducts_By_CategoryID_ResultObj.image = rdr["IMAGE"].ToString();
            //        f_getproducts_By_CategoryID_ResultObj.ProductId = Convert.ToInt32(rdr["ProductId"]);
            //        f_getproducts_By_CategoryID_ResultObj.ProductGUID = new Guid(rdr["ProductGUID"].ToString());
            //        f_getproducts_By_CategoryID_ResultObj.ProductName = rdr["ProductName"].ToString();
            //        f_getproducts_By_CategoryID_ResultObj.Category_Id = Convert.ToInt32(rdr["Category_Id"]);
            //        f_getproducts_By_CategoryID_ResultObj.category_name = rdr["category_name"].ToString();
            //        f_getproducts_By_CategoryID_ResultObj.categoryurlpath = rdr["categoryurlpath"].ToString();
            //        f_getproducts_By_CategoryID_ResultObj.Price = Convert.ToDecimal(rdr["Price"]);
            //        f_getproducts_By_CategoryID_ResultObj.OldPrice = Convert.ToDecimal(rdr["OldPrice"]);
            //        f_getproducts_By_CategoryID_ResultObj.maincaturlpath = rdr["maincaturlpath"].ToString();
            //        f_getproducts_By_CategoryID_ResultObj.maincatid = Convert.ToInt32(rdr["maincatid"]);
            //        f_getproducts_By_CategoryID_ResultList.Add(f_getproducts_By_CategoryID_ResultObj);
            //    }

            //    rdr.Close();
            //}
            string sql = "select * from f_cat_getproducts_By_OrgID(@ID)";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@ID", Value = orgId }
            };
             return await _dbContext.Set<f_getproducts_By_CategoryID_Result>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
            //return f_getproducts_By_CategoryID_ResultList;
        }
        public async Task<List<CategoryMaster>> GetCategories(int CategoryId,int orgId)
        {
            return await _dbContext.CategoryMaster.Where(x=>x.OrgId== orgId).ToListAsync();
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
        public async Task<List<f_getproducts_By_CategoryID>> GetAllProductByCategory(int CategoryId)
        {
            string sql = "select * from f_getproducts_By_CatID(@ID)";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@ID", Value = CategoryId }
            };
            return await _dbContext.Set<f_getproducts_By_CategoryID>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
        }
        public async Task<List<SP_GetSpecificationData_AttributName>> GetAllAttributes(int orgID)
        {

            string sql = "exec SP_GetSpecificationData_AttributName @OrgId";
            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@OrgId", Value = orgID }
            };
            return await _dbContext.Set<SP_GetSpecificationData_AttributName>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
        }
        public async Task<List<f_getproducts_By_CatID_SpecificationName>> GetAllProductByAttribute(int CategoryId, string SpecificationName)
        {
            string sql = "select * from f_getproducts_By_CatID_SpecificationName(@ID,@Name)";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@ID", Value = CategoryId },
                new SqlParameter { ParameterName = "@Name", Value = SpecificationName }
            };
            return await _dbContext.Set<f_getproducts_By_CatID_SpecificationName>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
        }
       
    }
}
