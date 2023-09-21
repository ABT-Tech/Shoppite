using Microsoft.AspNetCore.Http;
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
        private readonly ICommonHelper _commonHelper;
        private IHttpContextAccessor _accessor;
        List<CartModel> cartModel = new List<CartModel>();

        public CartViewComponent(ICartPageServices CartPageServices, ICommonHelper commonHelper, IHttpContextAccessor accessor)
        {
            // cartModel = cartModel;
            _CartPageServices = CartPageServices;
            _commonHelper = commonHelper;
            _accessor = accessor;
            // _commonHelper = commonHelper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int orgid = _commonHelper.GetOrgID(HttpContext);
            var username = _accessor.HttpContext.User.Identity.Name.ToString();
            var GetOrderBasic = await _CartPageServices.OrderBasic(username);
            var model = cartModel;
            return View("CartShow", GetOrderBasic);
        }
    }
}
