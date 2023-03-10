using AutoMapper;
using Shoppite.Application.Interfaces;
using Shoppite.Application.Models;
using Shoppite.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.Web.Services
{
    public class WishlistPageService:IWishlistPageService
    {
        private readonly IWishlistService _productService;
        private readonly IMapper _mapper;
        public WishlistPageService(IWishlistService productService, IMapper mapper)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<Customer_WishlistModel>> GetWishList(string Username, int OrgId)
        {
            var wishlist = await _productService.GetWishList(Username, OrgId);
            return wishlist;
        }
        public async Task AddWishList(MainModel wishlist, int ProductId)
        {
            await _productService.AddWishList(wishlist, ProductId);

        }
        public async Task<List<F_Orders_All_Model>> GetMyOrders(int OrgId,int ProfileId)
        {
            var orders = await _productService.GetMyOrders(OrgId, ProfileId);
            return orders;
        }
        public async Task<List<F_Pending_Orders_Model>> GetPendingOrders(int OrgId, int ProfileId)
        {
            var orders = await _productService.GetPendingOrders(OrgId, ProfileId);
            return orders;
        }
        public async Task<List<F_Pending_Orders_Model>> GetDeliveredOrders(int OrgId, int ProfileId)
        {
            var orders = await _productService.GetDeliveredOrders(OrgId, ProfileId);
            return orders;
        }
        public async Task<List<F_Pending_Orders_Model>> GetCancelledOrders(int OrgId, int ProfileId)
        {
            var orders = await _productService.GetCancelledOrders(OrgId, ProfileId);
            return orders;
        }
    }
}
