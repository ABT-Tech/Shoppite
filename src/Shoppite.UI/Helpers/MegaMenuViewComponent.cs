using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Models;
using Shoppite.UI.Interfaces;
using Shoppite.Web.Interfaces;

namespace Shoppite.UI.Helpers
{
    public class MegaMenuViewComponent:ViewComponent
    {

        private readonly ICategoryPageService _categoryPageService;
        private readonly CommonHelper commonHelper = new CommonHelper();

        MainModel MainModel = new MainModel();

        public MegaMenuViewComponent(ICategoryPageService categoryPageService)
        {
            _categoryPageService = categoryPageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int orgid = commonHelper.GetOrgID(HttpContext);
            MainModel.ProductsDetails = await _categoryPageService.GetProductList(orgid);
            MainModel.Categories = await _categoryPageService.GetCategories(0);
            return View("MegaMenu", MainModel);
        }
    }
}
