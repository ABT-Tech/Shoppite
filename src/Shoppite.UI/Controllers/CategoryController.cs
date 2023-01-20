using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Models;
using Shoppite.Core.Entities;
using Shoppite.UI.Helpers;
using Shoppite.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryPageService _categoryPageService;
        private readonly CommonHelper commonHelper = new CommonHelper();
        public CategoryController(ICategoryPageService categoryPageService)
        {
            _categoryPageService = categoryPageService ?? throw new ArgumentNullException(nameof(categoryPageService));
        }
        /* public IActionResult Index()
         {

             return View();
         }*/
        public async Task<ActionResult> Index()
        {
            var model = new CategoryMasterModel();
            var orgid = commonHelper.GetOrgID(HttpContext);
            
            model= await _categoryPageService.GetBannerImage(orgid);
            model= await _categoryPageService.GetBottomImage(orgid);
            
            model.CategoryDetails = await _categoryPageService.GetCategoryList(orgid);
           // model.CategoryDetails = await _categoryPageService.GetProductList(orgid);
            return View(model);
        }
    }
}
