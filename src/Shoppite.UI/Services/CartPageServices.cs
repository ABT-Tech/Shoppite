using AutoMapper;
using Shoppite.Application.Interfaces;
using Shoppite.Application.Models;
using Shoppite.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Services
{
    public class CartPageServices:ICartPageServices
    {
        private readonly ICartServices _CartService;
        private readonly IMapper _mapper;

        public CartPageServices(ICartServices CartService, IMapper mapper)
        {
            _CartService = CartService ?? throw new ArgumentNullException(nameof(CartService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CartModel> OrderBasic(int orgid)
        {
            return await _CartService.Orderbasic(orgid);
        }
        public async Task<CartModel> DeleteAysnc(int id)
        {
            return await _CartService.Delete(id);
        }
    }
}
