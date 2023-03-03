using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Http;
using Shoppite.Application.Interfaces;
using Shoppite.Application.Mapper;
using Shoppite.Application.Models;
using Shoppite.Core.Entities;
using Shoppite.Core.Interfaces;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Services
{
    public class CartServices : ICartServices
    {
        private readonly ICartRepository _CartRepository;
        private readonly IAppLogger<CartServices> _logger;
        private IHttpContextAccessor _accessor;
        public CartServices(ICartRepository CartRepository, IAppLogger<CartServices> appLogger)
        {
            _CartRepository = CartRepository ?? throw new ArgumentNullException(nameof(CartRepository));
            _logger = appLogger ?? throw new ArgumentNullException(nameof(appLogger));
        }

        public async Task<CartModel> Orderbasic(int orgid)
        {
            CartModel orderbasic = new CartModel();
            var Orderbasic = await _CartRepository.OrderBasic(orgid);
            orderbasic.F_Getproduct_CartDetails_By_Orgids = ObjectMapper.Mapper.Map<List<f_getproduct_CartDetails_By_OrgidModel>>(Orderbasic);
            return orderbasic;
        }
        public async Task<CartModel> Delete(int id)
        {
            var productDelete = await _CartRepository.DeleteAsync(id);
            var mapped = ObjectMapper.Mapper.Map<CartModel>(productDelete);
            return mapped;
        }

        public async Task SaveAddress(CartModel cartModel)
        {
            cartModel.OrderShippingModel.Contactnumber = cartModel.OrderShippingModel.Phone;
            cartModel.OrderShippingModel.InsertDate = DateTime.Now;

            var maapped = ObjectMapper.Mapper.Map<OrderShipping>(cartModel.OrderShippingModel);
            await _CartRepository.SaveAddress(maapped); 
        }

        public async Task UpdateOrder(CheckOutModel checkOut)
        {
            foreach(var update in checkOut.ar)
            {
               try
               {
                 Guid guid = Guid.Parse(update.Guid);
                 int qty = Convert.ToInt32(update.Qty);

                  OrderBasic orderBasic = new OrderBasic
                  {
                    OrderGuid = guid,
                    Qty = qty,
                  };
                await _CartRepository.UpdateOrder(orderBasic);
                }
                 catch (Exception e)
                 {

                    throw e;
                 }

            }
        }
    }
}
