using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shoppite.Application.Models;
using Shoppite.UI.Helpers;
using Shoppite.UI.Interfaces;
using Shoppite.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Controllers
{
    public class HomeController : Controller
    {
        private IHttpContextAccessor _accessor;

        private readonly ILogger<HomeController> _logger;
        private readonly IBrandPageServices _BrandPageService;
        private readonly IWishlistPageService _wishlistPageService;
        private readonly ICategoryPageService _categoryPageService;
        private readonly CommonHelper commonHelper = new CommonHelper();

        public HomeController(IBrandPageServices brandPageServices, ILogger<HomeController> logger, ICategoryPageService categoryPageService, IHttpContextAccessor accessor,IWishlistPageService wishlistPageService)
        {
            _accessor = accessor;
            _logger = logger ?? throw new ArgumentNullException();
            _BrandPageService = brandPageServices ?? throw new ArgumentNullException(nameof(brandPageServices));
            _categoryPageService = categoryPageService ?? throw new ArgumentNullException(nameof(categoryPageService));
            _wishlistPageService = wishlistPageService ?? throw new ArgumentNullException(nameof(wishlistPageService));
        }
        public async Task<IActionResult>Index(int CategoryId)
        {
            int OrgId  = commonHelper.GetOrgID(HttpContext);
            var brands = await _BrandPageService.GetBrands(OrgId);
            brands.CategoryMaster =  await _categoryPageService.DisplayLogo(OrgId);
            brands.MiddelBanner = await _categoryPageService.GetMiddelBannerImage(OrgId);
          //  brands.TopBanner = await _categoryPageService.GetTopBannerImage(OrgId);
            brands.ProductsDetails = await _categoryPageService.GetProductList(OrgId);
            brands.Categories = await _categoryPageService.GetCategories(CategoryId,OrgId);
            brands.HorizontalBanner = await _categoryPageService.GetHorizontalBanner(OrgId);
            brands.TopBanner = await _categoryPageService.GetBannerByCategory(OrgId);
            return View(brands);
        }
        public async Task<IActionResult> ProductsByBrand(int CategoryId,int BrandId)
        {
            int OrgId = commonHelper.GetOrgID(HttpContext);
            var brands = await _BrandPageService.GetBrands(OrgId);
            brands.CategoryMaster = await _categoryPageService.DisplayLogo(OrgId);
            brands.MiddelBanner = await _categoryPageService.GetMiddelBannerImage(OrgId);
            //  brands.TopBanner = await _categoryPageService.GetTopBannerImage(OrgId);
            brands.ProductsDetails = await _categoryPageService.GetProductList(OrgId);
            brands.Categories = await _categoryPageService.GetCategories(CategoryId, OrgId);
            brands.HorizontalBanner = await _categoryPageService.GetHorizontalBanner(OrgId);
            brands.TopBanner = await _categoryPageService.GetBannerByCategory(OrgId);
            brands.ProductdByBrand = await _BrandPageService.GetProductsByBrand(OrgId, BrandId);
            return View(brands);
        }
        [HttpGet]
        public async Task<JsonResult> Get_Product_By_Cat(int ID)
        {
           var AA = await _BrandPageService.Get_Product_By_Cat(ID);
            return Json(AA.F_Getproducts_By_CategoryIDModels);
        }
        public async Task<IActionResult> AllProducts(int CategoryId)
        {
            int OrgId = commonHelper.GetOrgID(HttpContext);
            var brands = await _BrandPageService.GetBrands(OrgId);
            brands.CategoryMaster = await _categoryPageService.DisplayLogo(OrgId);
            brands.ProductsDetails = await _categoryPageService.GetProductList(OrgId);
            brands.Categories = await _categoryPageService.GetCategories(CategoryId, OrgId);
            brands.Product_specification = await _categoryPageService.GetAllProductByCategory(CategoryId,OrgId);
            brands.Wishlists = await _wishlistPageService.GetWishList("admin", OrgId);
            brands.Attributes = await _categoryPageService.GetAllAttributes(OrgId);
           // brands.AllCategories = await _categoryPageService.GetAllCategories(OrgId);
            return View(brands);

        }
        [HttpGet]
        public async Task<IActionResult> _ProductsByAttribute(int CategoryId, string SpecificationName)
        {
            int OrgId = commonHelper.GetOrgID(HttpContext);
            MainModel model = new MainModel();
            if(SpecificationName!=null)
            {
                model.Product_specification = await _categoryPageService.GetAllProductByAttribute(CategoryId, SpecificationName);
            }
            else
            {
                model.Product_specification = await _categoryPageService.GetAllProductByCategory(CategoryId,OrgId);
            }
            return PartialView(model);
        }
    }
}
