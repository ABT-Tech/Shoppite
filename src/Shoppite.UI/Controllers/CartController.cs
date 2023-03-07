using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Models;
using Shoppite.UI.Helpers;
using Shoppite.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Controllers
{
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
            await _cartPageService.UpdateOrder(checkOut);
            return Json(checkOut);
        }

        public async Task<ActionResult> CheckOut()
        {
            return View();
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }

        public async Task<IActionResult> SaveAddress(CartModel cartModel)
        {
             await _cartPageService.SaveAddress(cartModel);
            return RedirectToAction("CheckOut");
        }
    }
}
