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
        private readonly ICommonHelper _commonHelper;

        public CartController(ICartPageServices cartPageServices, ICommonHelper commonHelper)
        {
            _cartPageService = cartPageServices ?? throw new ArgumentNullException(nameof(cartPageServices));
            _commonHelper = commonHelper;
        }
       
        public async Task<IActionResult> Cart()
        {
            int orgid = _commonHelper.GetOrgID(HttpContext);
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

        public async Task<IActionResult> OrderSuccess()
        
        {
            // await _cartPageService.UpdateOrder(orderid);Guid orderid
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveAddress(CartModel Model)
        {
            int orgid = _commonHelper.GetOrgID(HttpContext);
            Model.OrderShippingModel.OrgId = orgid;
            await _cartPageService.SaveAddress(Model);

            await _cartPageService.UpdateOrder((Guid)Model.OrderBasicModel.OrderGuid);

            return RedirectToAction("OrderSuccess");
        }
    }
}
