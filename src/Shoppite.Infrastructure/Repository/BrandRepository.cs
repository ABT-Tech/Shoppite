using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shoppite.Core.Entities;
using Shoppite.Core.Repositories;
using Shoppite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Infrastructure.Repository
{
   public class BrandRepository:IBrandRepository
   {
        protected readonly Shoppite_masterContext _dbContext;

        public BrandRepository(Shoppite_masterContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Brands>> GetBrands(int orgid)
        {
          var  brands = await _dbContext.Brands.Where(x => x.OrgId == orgid).ToListAsync();
             return brands;
        }

        public async Task<List<f_getproducts_By_OrgID>> GetCategoryBy_Org(int orgid)
        {
            string sql = "select * from f_getproducts_By_OrgID(@orgid)";

            List<SqlParameter> parms = new List<SqlParameter>
             { 
                // Create parameters    
                new SqlParameter { ParameterName = "@orgid", Value = orgid }
             };

            return await _dbContext.Set<f_getproducts_By_OrgID>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
        }

        public async Task<List<f_getproducts_By_NewArrivals>> _Getproducts_By_NewArrivals(int orgid)
        {

            string sql = "select * from f_getproducts_By_statusspecification(@orgid)";

            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@orgid", Value = orgid }
            };

            var productlist = await _dbContext.Set<f_getproducts_By_NewArrivals>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();

            var filter = productlist.GroupBy(x => x.StatusId).SelectMany(x=>x).ToList();

            return filter;
        }

        public async Task<List<f_getproducts_By_CategoryID>> Get_Product_By_Cat(int ID)
        {

            string sql = "select * from f_getproducts_By_CategoryID(@ID)";

            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@ID", Value = ID }
            };

            return await _dbContext.Set<f_getproducts_By_CategoryID>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
        }
        public async Task<List<sp_getcat_Result>> Sp_Getcat(int orgid)
        {
            string sql = "EXEC sp_getcat @orgid";

            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@orgid", Value = orgid }
            };
            var CatList = await _dbContext.Set<sp_getcat_Result>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();

            var FilterCat = CatList.Where(x => x.PARENT_NAMEID != 0).ToList();

            return FilterCat;
        }

        public async Task<List<CategoryMaster>> CategoryMaster(int orgid)
        {
            var brands = await _dbContext.CategoryMaster.Where(x => x.OrgId == orgid).ToListAsync();

            var FilterCat = brands.Where(x => x.ParentCategoryId != 0).ToList();
            return FilterCat;
        }

        public async Task<List<f_getproducts_Recentlyviewed>> F_Getproducts_Recentlyviewed(string id, int orgid)
        {
            string sql = "select * from f_getproducts_Recentlyviewed(@IP,@orgid)";

            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@IP", Value = id },
                new SqlParameter { ParameterName = "@orgid", Value = orgid }
            };

            return await _dbContext.Set<f_getproducts_Recentlyviewed>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
        }

    }
}
