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
    public class ProductDetailController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IproductDetailPageServices _ProductDetailPageService;
        private readonly IWishlistPageService _productWishListService;

        //private readonly ICategoryPageService _categoryPageService;
        private readonly ICommonHelper _commonHelper;


        
        public ProductDetailController(ILogger<HomeController> logger, IproductDetailPageServices categoryPageService, ICommonHelper commonHelper, IWishlistPageService productWishListService)
        {
            _logger = logger ?? throw new ArgumentNullException();
            _ProductDetailPageService = categoryPageService ?? throw new ArgumentNullException(nameof(categoryPageService));
            _commonHelper = commonHelper;
            _productWishListService = productWishListService;

            //_categoryPageService = categoryPageService ?? throw new ArgumentNullException(nameof(categoryPageService));
        }

        public IActionResult Index()
        {
            return View();
        }

        // [HttpPost]
        public async Task<IActionResult> Details(Guid id)
        {
            int orgid = _commonHelper.GetOrgID(HttpContext);
            var Product_Details = await _ProductDetailPageService.GetProductDetails(id, orgid);
            return View(Product_Details);
        }

        public async Task<IActionResult> AddToWhishList(int ProductId,Guid id)
        {
            int OrgId = commonHelper.GetOrgID(HttpContext);
            MainModel mainModel = new MainModel();
            mainModel.ProductId = ProductId;
            mainModel.OrgId = OrgId;

            await _productWishListService.AddtowhishList(mainModel);

            return RedirectToAction("Details",new {id=id});
        }

        [HttpGet]
        public async Task<IActionResult> AddProductToCart(ProductDetailModel productDetailModel)
        {
            await _ProductDetailPageService.AddToCart(productDetailModel);
            return RedirectToAction("Details",new { id = productDetailModel.ProductBasicModel.ProductGuid });
        } 

    }
}
