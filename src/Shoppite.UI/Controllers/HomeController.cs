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
        private readonly ILogger<HomeController> _logger;
        private readonly IBrandPageServices _BrandPageService;
        private readonly ICategoryPageService _categoryPageService;
        private readonly CommonHelper commonHelper = new CommonHelper();

        public HomeController(IBrandPageServices brandPageServices, ILogger<HomeController> logger, ICategoryPageService categoryPageService)
        {
            _logger = logger ?? throw new ArgumentNullException();
            _BrandPageService = brandPageServices ?? throw new ArgumentNullException(nameof(brandPageServices));
            _categoryPageService = categoryPageService ?? throw new ArgumentNullException(nameof(categoryPageService));
        }

        //public async Task <IActionResult> Index()
        //{
        //    return View();
        //}
        //public async Task<IActionResult>NewProducts(MainModel mainModel)
        //{
        //    int orgid = commonHelper.GetOrgID(HttpContext);
            
        //    return RedirectToAction("Index");
        //}
        public async Task<IActionResult>Index(int CategoryId)
        {
            int OrgId  = commonHelper.GetOrgID(HttpContext);
            var brands = await _BrandPageService.GetBrands(OrgId);
            brands.CategoryMaster =  await _categoryPageService.DisplayLogo(OrgId);
            brands.MiddelBanner = await _categoryPageService.GetMiddelBannerImage(OrgId);
            brands.TopBanner = await _categoryPageService.GetTopBannerImage(OrgId);
            brands.ProductsDetails = await _categoryPageService.GetProductList(OrgId);
            brands.Categories = await _categoryPageService.GetCategories(CategoryId);
            brands.HorizontalBanner = await _categoryPageService.GetHorizontalBanner(OrgId);
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
            brands.ProductsDetails = await _categoryPageService.GetProductList(OrgId);
            brands.Categories = await _categoryPageService.GetCategories(CategoryId);
            brands.CategoryProduct = await _categoryPageService.GetAllProductByCategory(CategoryId);
            brands.SubCategories = await _categoryPageService.GetAllSubCategories(5048);
            brands.Attributes = await _categoryPageService.GetAllAttributes(OrgId);
            return View(brands);

        }
    }
}
