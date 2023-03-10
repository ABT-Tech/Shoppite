using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shoppite.Application.Models;
using Shoppite.UI.Helpers;
using Shoppite.UI.Interfaces;
using Shoppite.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistPageService _productPageService;
        private readonly CommonHelper commonHelper = new CommonHelper();
        private IHttpContextAccessor _accessor;
        private readonly ILogger<WishlistController> _logger;
        private readonly IBrandPageServices _BrandPageService;
        private readonly ICategoryPageService _categoryPageService;
        public WishlistController(IBrandPageServices brandPageServices,ICategoryPageService categoryPageService, ILogger<WishlistController> logger, IWishlistPageService productPageService, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _BrandPageService = brandPageServices ?? throw new ArgumentNullException(nameof(brandPageServices));
            _categoryPageService = categoryPageService ?? throw new ArgumentNullException(nameof(categoryPageService));
            _logger = logger ?? throw new ArgumentNullException();
            _productPageService = productPageService ?? throw new ArgumentNullException(nameof(productPageService));
        }
        [HttpGet]
        public async Task<IActionResult> Wishlist(int CategoryId, string Username)
        {
            int OrgId = commonHelper.GetOrgID(HttpContext);
            var brands = await _BrandPageService.GetBrands(OrgId);
            brands.CategoryMaster = await _categoryPageService.DisplayLogo(OrgId);
            brands.Categories = await _categoryPageService.GetCategories(CategoryId,OrgId);
            brands.ProductsDetails = await _categoryPageService.GetProductList(OrgId);
            brands.Wishlists=await _productPageService.GetWishList("admin", OrgId);
            return View(brands);
        }
        [HttpPost]
        public async Task<IActionResult> Wishlist(MainModel wishlist, int id)
        {
            var ipadresss = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            int OrgId = commonHelper.GetOrgID(HttpContext);
            wishlist.OrgId = OrgId;
            wishlist.Ip = ipadresss;
            wishlist.UserName = "admin";  
            await _productPageService.AddWishList(wishlist, id);
            return View(wishlist);
        }
    }
}
