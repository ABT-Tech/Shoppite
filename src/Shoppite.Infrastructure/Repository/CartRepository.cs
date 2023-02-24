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
   public class CartRepository : ICartRepository
   {
        protected readonly Shoppite_masterContext _dbContext;

        public CartRepository(Shoppite_masterContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<f_getproduct_CartDetails_By_Orgid>> OrderBasic(int orgid)
        {
            string sql = "select * from f_getproduct_CartDetails_By_Orgid(@Orgid)";

            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@Orgid", Value = orgid }
            };
            return await _dbContext.Set<f_getproduct_CartDetails_By_Orgid>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
        }
        public async Task<OrderBasic> DeleteAsync(int id)
        {
            var product = _dbContext.OrderBasic.FirstOrDefault(x => x.ProductId == id);
            if (product != null)
            {
                _dbContext.OrderBasic.Remove(product);
                _dbContext.SaveChanges();
            }
            return product;
        }

    }
}
