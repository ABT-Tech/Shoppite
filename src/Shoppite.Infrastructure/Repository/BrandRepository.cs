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

        public async Task<List<F_getproducts_By_CatId>> Get_Product_By_Cat(int ID)
        {

            string sql = "select * from f_getproducts_By_CatID(@ID)";

            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@ID", Value = ID }
            };

            return await _dbContext.Set<F_getproducts_By_CatId>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
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

        public async Task<List<F_getproducts_By_BrandId>> GetProductsByBrand(int orgid,int BrandId)
        {
            string sql = "select * from f_getproducts_By_BrandID(@ID,@OrgId)";

            List<SqlParameter> parms = new List<SqlParameter>
             { 
                // Create parameters    
                new SqlParameter { ParameterName = "@orgid", Value = orgid },
                 new SqlParameter { ParameterName = "@ID", Value = BrandId }
             };

           var aaa =  await _dbContext.Set<F_getproducts_By_BrandId>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
            return aaa;
        }

        public async Task News_Letter_Submit(int orgid, string email)
        {
            NewsLetter newsLetter = new NewsLetter();
            var check = await _dbContext.NewsLetter.Where(x => x.Email == email && x.OrgId == orgid).FirstOrDefaultAsync();
            if(check == null)
            {
                newsLetter.Email = email;
                newsLetter.OrgId = orgid;
                newsLetter.InsertDate = DateTime.Now;
                await _dbContext.NewsLetter.AddAsync(newsLetter);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ProductBasic>> SearchProduct(string searchKey,int OrgId)
        {
            var Search = await _dbContext.ProductBasic.Where(x=>x.OrgId==OrgId&&x.ProductName.Contains(searchKey)).ToListAsync();
            return Search;
        }

        public async Task<ProductPrice> GetPrice(Guid productGuid)
        {
            return await _dbContext.ProductPrice.FirstOrDefaultAsync(x => x.ProductGuid == productGuid);
        }

        public async Task<f_order_master> GetMyOrders(int orderid)
        {
            string sql = "select * from f_order_master()";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                //new SqlParameter { ParameterName = "@orgid", Value = Orgid },
                //new SqlParameter { ParameterName = "@profileid", Value = ProfileId }
            };

            return await _dbContext.Set<f_order_master>().FromSqlRaw(sql, parms.ToArray()).
                  Where(x =>x.OrderId == orderid).FirstOrDefaultAsync();
        }
        public async Task<List<f_order_master>> GetOrderedproductDetails(int orderid)
        {
            string sql = "select * from f_order_master()";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                //new SqlParameter { ParameterName = "@orgid", Value = Orgid },
                //new SqlParameter { ParameterName = "@profileid", Value = ProfileId }
            };

            return await _dbContext.Set<f_order_master>().FromSqlRaw(sql, parms.ToArray()).Where(x=>x.OrderId== orderid).ToListAsync();
        }

        public async Task<OrderShipping> GetOrderShipping(Guid? orderGUID)
        {
            return await _dbContext.OrderShipping.Where(x => x.OrderGuid == orderGUID).FirstOrDefaultAsync();
        }

        public async Task<UsersProfile> GetShippingDetail(string userName,int orgid)
        {
            return await _dbContext.UsersProfile.Where(x => x.UserName == userName && x.Type == "Client" && x.OrgId==orgid).FirstOrDefaultAsync();
        }

        public async Task<List<ProductBasic>> GetProductDetail(string productName, string coverImage)
        {
            return await _dbContext.ProductBasic.Where(x => x.ProductName == productName && x.CoverImage == coverImage).ToListAsync();
        }

        public async Task CancleOrder(int orderid)
        {
           var findOrder = await _dbContext.OrderStatus.Where(x => x.OrderId == orderid).FirstOrDefaultAsync();

            if(findOrder != null)
            {
                var local = _dbContext.Set<OrderStatus>().Local.FirstOrDefault(entry => entry.OrderId.Equals(orderid));

                if(local != null)
                {
                  _dbContext.Entry(local).State = EntityState.Detached;
                }

                findOrder.OrderStatus1 = "Cancelled";

                _dbContext.Entry(findOrder).State = EntityState.Modified;

              // _dbContext.OrderStatus.Update(findOrder);
            } 

            var FindFromOrderBasic = await _dbContext.OrderBasic.Where(x => x.OrderId == orderid).FirstOrDefaultAsync();

            if(FindFromOrderBasic != null)
            {
                var local = _dbContext.Set<OrderBasic>().Local.FirstOrDefault(entry => entry.OrderId.Equals(orderid));

                if (local != null)
                {
                    _dbContext.Entry(local).State = EntityState.Detached;
                }
                FindFromOrderBasic.LastOrderStatus = "Cancelled";

                _dbContext.Entry(FindFromOrderBasic).State = EntityState.Modified;
            }

            var Inventory = await _dbContext.ProductBasic.Where(x => x.ProductId == FindFromOrderBasic.ProductId).FirstOrDefaultAsync();

            if (Inventory != null)
            {
                var local = _dbContext.Set<ProductBasic>().Local.FirstOrDefault(x => x.ProductId.Equals(Inventory.ProductId));
                if(local != null)
                {
                    _dbContext.Entry(local).State = EntityState.Detached;
                }
                Inventory.Qty = Inventory.Qty + FindFromOrderBasic.Qty;

                _dbContext.Entry(Inventory).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Users> GetUser(string email,int orgid)
        {
            return await _dbContext.Users.Where(x => x.Email == email && x.OrgId == orgid ).FirstOrDefaultAsync();
        }

        public async Task<OrderStatus> GetOrderStatus(int orderid,int orgid)
        {
            return await _dbContext.OrderStatus.Where(x => x.OrderId == orderid && x.OrgId == orgid).FirstOrDefaultAsync();
        }
    }
}
