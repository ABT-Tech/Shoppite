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
    public class OrdersController : Controller
    {
        private readonly IWishlistPageService _productPageService;
        private readonly ICommonHelper _commonHelper;
        private IHttpContextAccessor _accessor;
        private readonly ILogger<OrdersController> _logger;
        private readonly IBrandPageServices _BrandPageService;
        private readonly ICategoryPageService _categoryPageService;
        public OrdersController(IBrandPageServices brandPageServices, ICategoryPageService categoryPageService, ILogger<WishlistController> logger, IWishlistPageService productPageService, IHttpContextAccessor accessor, ICommonHelper commonHelper)
        {
            _accessor = accessor;
            _BrandPageService = brandPageServices ?? throw new ArgumentNullException(nameof(brandPageServices));
            _categoryPageService = categoryPageService ?? throw new ArgumentNullException(nameof(categoryPageService));
            //_logger = logger ?? throw new ArgumentNullException();
            _productPageService = productPageService ?? throw new ArgumentNullException(nameof(productPageService));
            _commonHelper = commonHelper;
        }
        [HttpGet]
        public async Task<IActionResult> MyOrders(int CategoryId, string Username,int ProfileId)
        {
            int OrgId = _commonHelper.GetOrgID(HttpContext);
            var brands = await _BrandPageService.GetBrands(OrgId);
            brands.CategoryMaster = await _categoryPageService.DisplayLogo(OrgId);
            brands.Categories = await _categoryPageService.GetCategories(CategoryId,OrgId);
            brands.ProductsDetails = await _categoryPageService.GetProductList(OrgId);
            brands.Wishlists = await _productPageService.GetWishList("admin", OrgId);
            brands.MyOrders = await _productPageService.GetMyOrders(OrgId,1097);
           // brands.PendingOrders = await _productPageService.GetPendingOrders(OrgId, 1097);
            return View(brands);
        }
        [HttpGet]
        public async Task<IActionResult> _PendingOrders(int profileid)
        {
            MainModel model = new MainModel();
            int OrgId = _commonHelper.GetOrgID(HttpContext);
            model.Orders = await _productPageService.GetPendingOrders(OrgId, 1097);         
            return PartialView(model);
        }
        [HttpGet]
        public async Task<IActionResult> _AllOrders(int profileid)
        {
            MainModel model = new MainModel();
            int OrgId = _commonHelper.GetOrgID(HttpContext);
            model.MyOrders = await _productPageService.GetMyOrders(OrgId, 1097);
            return PartialView(model);
        }
        [HttpGet]
        public async Task<IActionResult> _CancelledOrders(int profileid)
        {
            MainModel model = new MainModel();
            int OrgId = _commonHelper.GetOrgID(HttpContext);
            model.Orders = await _productPageService.GetCancelledOrders(OrgId, 1097);
            return PartialView(model);
        }
        [HttpGet]
        public async Task<IActionResult> _DeliveredOrders(int profileid)
        {
            MainModel model = new MainModel();
            int OrgId = _commonHelper.GetOrgID(HttpContext);
            model.Orders = await _productPageService.GetDeliveredOrders(OrgId, 1097);
            return PartialView(model);
        }
    }
}
