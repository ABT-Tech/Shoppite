﻿using Microsoft.Data.SqlClient;
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
            var product = _dbContext.OrderBasic.FirstOrDefault(x => x.OrderId == id && x.OrderStatus == "Cart");
            if (product != null)
            {
                _dbContext.OrderBasic.Remove(product);
                _dbContext.SaveChanges();
            }
            return product;
        }

        public async Task SaveAddress(OrderShipping orderShipping)
        {
            //var OrderCheck = _dbContext.OrderShipping.FirstOrDefault(x => x.Contactnumber == orderShipping.Contactnumber);
            //if(OrderCheck == null)
            //{
                _dbContext.OrderShipping.Add(orderShipping);
           // }
           await _dbContext.SaveChangesAsync();

        }

        //public async Task<OrderBasic> CheckOrder(OrderBasic orderBasic)
        //{
        //    var check = await _dbContext.OrderBasic.FirstOrDefaultAsync(x => x.OrderGuid == orderBasic.OrderGuid && x.OrderStatus == "Cart");

        //    if(check != null)
        //    {
        //        check.Qty = orderBasic.Qty;

        //        _dbContext.OrderBasic.Update(check);
        //    }
        // await _dbContext.SaveChangesAsync();
        //}
        public async Task<OrderBasic> CheckOrder(OrderBasic orderBasic)
        {
            var check = await _dbContext.OrderBasic.FirstOrDefaultAsync(x => x.OrderGuid == orderBasic.OrderGuid && x.OrderStatus == "Cart" && x.UserName == orderBasic.UserName);
            return check;
        }

        public async Task UpdateOrder(OrderBasic orderBasic)
        {
            var check = await _dbContext.OrderBasic.Where(x => x.OrderGuid == orderBasic.OrderGuid && x.OrderStatus == "Cart" && x.UserName == orderBasic.UserName).ToListAsync();

            foreach (var order in check)
            {
                order.OrderStatus = "Confirmed";
                order.ReferenceId = "COD";
                order.PaymentMode = "Cash On Delivery";
                order.LastOrderStatus = "Pending";
                order.InsertDate = DateTime.Now;

                _dbContext.OrderBasic.Update(order);

                var Inventory = await _dbContext.ProductBasic.Where(x => x.ProductId == order.ProductId && x.OrgId == order.OrgId).FirstOrDefaultAsync();

                if (Inventory != null)
                {
                    var local = _dbContext.Set<ProductBasic>().Local.FirstOrDefault(x => x.ProductId.Equals(Inventory.ProductId));
                    if (local != null)
                    {
                        _dbContext.Entry(local).State = EntityState.Detached;
                    }
                    Inventory.Qty = Inventory.Qty - order.Qty;

                    _dbContext.Entry(Inventory).State = EntityState.Modified;
                }
                await _dbContext.SaveChangesAsync();
            }

            var getOrderMster = await _dbContext.OrderMaster.Where(x => x.OrderGuid == orderBasic.OrderGuid).FirstOrDefaultAsync();
            if (getOrderMster != null)
            {
                var StatusCheck = await _dbContext.OrderStatus.FirstOrDefaultAsync(x => x.OrderId == getOrderMster.OrderMasterId);
                if (StatusCheck == null)
                {
                    OrderStatus orderStatus = new OrderStatus
                    {
                        OrderId = getOrderMster.OrderMasterId,
                        OrderStatus1 = "Pending",
                        StatusDate = DateTime.Now,
                        Remarks = string.Empty,
                        Insertby = DateTime.Now.ToString(),
                        OrgId = getOrderMster.OrgId,

                    };
                    _dbContext.OrderStatus.Add(orderStatus);
                }
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrderQty(OrderBasic orderBasic)
        {
            var check = await _dbContext.OrderBasic.Where(x => x.OrderGuid == orderBasic.OrderGuid && x.OrderStatus == "Cart" && x.UserName == orderBasic.UserName && x.ProductId == orderBasic.ProductId).FirstOrDefaultAsync();
            if(check != null)
            {
                check.Qty = orderBasic.Qty;
            }
            _dbContext.OrderBasic.Update(check);

            await _dbContext.SaveChangesAsync();

        }

        public async Task<UsersProfile> FindAddress(string userName)
        {
            var find = await _dbContext.UsersProfile.FirstOrDefaultAsync(x => x.UserName == userName);
            return find;
        }

        public async Task<UsersProfile> GetVendorDetails(UsersProfile usersProfile)
        {
            return await _dbContext.UsersProfile.FirstOrDefaultAsync(x => x.OrgId == usersProfile.OrgId);
        }

        public async Task<OrderShipping> GetAddredd(string userName)
        {
            var find = await _dbContext.OrderShipping.FirstOrDefaultAsync(x => x.UserName == userName && x.FirstName != null && x.LastName != null && x.Address != null);
            return find;
        }

        public async Task<f_getproduct_CartDetails_By_Orgid> CheckProdInCart(int orgId, string ProductName, string Username,int SpecId)
        {
            string sql = "select * from f_getproduct_CartDetails_By_Orgid(@Orgid)";

            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@Orgid", Value = orgId }
            };
            var Filter =  await _dbContext.Set<f_getproduct_CartDetails_By_Orgid>().FromSqlRaw(sql, parms.ToArray()).Where(x=>x.UserName == Username && x.ProductName == ProductName && x.SpecificationId == SpecId).FirstOrDefaultAsync();
            return Filter;
        }
    }
}
