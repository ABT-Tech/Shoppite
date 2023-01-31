using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shoppite.Application.Models;
using Shoppite.UI.Helpers;
using Shoppite.UI.Interfaces;
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
        private readonly CommonHelper commonHelper = new CommonHelper();

        public HomeController(IBrandPageServices brandPageServices, ILogger<HomeController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException();
            _BrandPageService = brandPageServices ?? throw new ArgumentNullException(nameof(brandPageServices));
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
        public async Task<IActionResult>Index()
        {
          int OrgId  = commonHelper.GetOrgID(HttpContext);
          var brands = await _BrandPageService.GetBrands(OrgId);
            return View(brands);
        }
       [HttpGet]
        public async Task<JsonResult> Get_Product_By_Cat(int ID)
        {
           var AA = await _BrandPageService.Get_Product_By_Cat(ID);
            return Json(AA.F_Getproducts_By_CategoryIDModels);
        }
    }
}
