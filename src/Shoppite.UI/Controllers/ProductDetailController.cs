using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shoppite.Application.Models;
using Shoppite.UI.Helpers;
using Shoppite.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IproductDetailPageServices _ProductDetailService;
        //private readonly ICategoryPageService _categoryPageService;
        private readonly CommonHelper commonHelper = new CommonHelper();


        public ProductDetailController(ILogger<HomeController> logger, IproductDetailPageServices categoryPageService)
        {
            _logger = logger ?? throw new ArgumentNullException();
            _ProductDetailService = categoryPageService ?? throw new ArgumentNullException(nameof(categoryPageService));

            //_categoryPageService = categoryPageService ?? throw new ArgumentNullException(nameof(categoryPageService));
        }

        public IActionResult Index()
        {
            return View();
        }

        // [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            int orgid =commonHelper.GetOrgID(HttpContext);
            var Product_Details = await _ProductDetailService.GetProductDetails(id, orgid);
            return View("Details", Product_Details);
        }

    }
}
