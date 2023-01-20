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

        public async Task<IQueryable<f_getproducts_By_NewArrivals>> GetNewProducts(int orgid)
        {
            //string sql = "select * from f_getproducts_By_getmegaoffers(@orgid)";

            //List<SqlParameter> parms = new List<SqlParameter>
            //{ 
            //    // Create parameters    
            //    new SqlParameter { ParameterName = "@orgid", Value = orgid }
            //};

            //var product = await _dbContext.Set<f_getproducts_By_NewArrivals>().FromSqlRaw(sql,parms.ToArray()).ToListAsync();
            //var q = product.AsQueryable().Select(x=> new f_getproducts_By_NewArrivals
            //        //(from prop in product
            //        // orderby prop.ModifiedDate ?? prop.InsertDate descending
            //        //// where prop.deals.Contains("New-Arrival")
            //        // select new f_getproducts_By_NewArrivals
            //         {
            //             ProductId = x.ProductId,
            //             URLPath = x.URLPath,
            //             image = x.image,
            //             ProductName = x.ProductName,
            //             Price = x.Price,
            //             CurrencyName = x.CurrencyName,
            //             totalpick = x.totalpick
            //         });


            return null;
        }

        public async Task<List<f_getproducts_By_NewArrivals>> _Getproducts_By_NewArrivals(int orgid)
        {

            string sql = "select * from f_getproducts_By_getmegaoffers(@orgid)";

            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@orgid", Value = orgid }
            };

            return await _dbContext.Set<f_getproducts_By_NewArrivals>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
        }
    }
}
