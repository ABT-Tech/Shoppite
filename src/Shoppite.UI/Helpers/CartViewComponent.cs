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
        private readonly CommonHelper commonHelper = new CommonHelper(); 

        List<CartModel> cartModel = new List<CartModel>();

        public CartViewComponent(ICartPageServices CartPageServices)
        {
            // cartModel = cartModel;
            _CartPageServices = CartPageServices;
           // _commonHelper = commonHelper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int orgid = commonHelper.GetOrgID(HttpContext);
            var GetOrderBasic = await _CartPageServices.OrderBasic(orgid);
            var model = cartModel;
            return View("CartShow", GetOrderBasic);
        }
    }
}
