using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Models;
using Shoppite.UI.Extensions;
using Shoppite.UI.Helpers;
using Shoppite.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartPageServices _cartPageService;
        private readonly CommonHelper commonHelper = new CommonHelper();

        public CartController(ICartPageServices cartPageServices)
        {
            _cartPageService = cartPageServices ?? throw new ArgumentNullException(nameof(cartPageServices));
        }
       
        public async Task<IActionResult> Cart()
        {
            int orgid = commonHelper.GetOrgID(HttpContext);
            var cartlist = await _cartPageService.OrderBasic(orgid);
            return View(cartlist);
        }

        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            _cartPageService.DeleteAysnc(id);
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public async Task<ActionResult> AddToCheck([FromBody] CheckOutModel checkOut )
        {
          await _cartPageService.UpdateOrderQty(checkOut);

            return Json(checkOut);
        }

        public async Task<ActionResult> CheckOut(Guid orderid)
        {
         var order =  await _cartPageService.CheckOrder(orderid);
            //  var orderId = Request.Query
            return View(order);
        }

        public async Task<ActionResult> OrderSuccessAsync(Guid orderid)
        {
            await _cartPageService.UpdateOrder(orderid);

            return View();
        }

        public async Task<IActionResult> SaveAddress(CartModel cartModel)
        {
             await _cartPageService.SaveAddress(cartModel);
            return RedirectToAction("CheckOut");
        }
    }
}
