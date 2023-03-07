using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Models;
using Shoppite.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Helpers
{
    public class CartViewComponent:ViewComponent
    {
        private readonly ICartPageServices _CartPageServices;
        private readonly CommonHelper _commonHelper; 

        List<CartModel> cartModel = new List<CartModel>();

        public CartViewComponent(ICartPageServices CartPageServices)
        {
            // cartModel = cartModel;
            _CartPageServices = CartPageServices;
           // _commonHelper = commonHelper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //int orgid = _commonHelper.GetOrgID(HttpContext);
            var GetOrderBasic = await _CartPageServices.OrderBasic(1);
            var model = cartModel;
            return View("CartShow", GetOrderBasic);
        }
    }
}
