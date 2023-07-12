using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shoppite.Application.Models;
using Shoppite.UI.Helpers;
using Shoppite.UI.Interfaces;
using Shoppite.UI.Models;
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
            int OrgId = _commonHelper.GetOrgID(HttpContext);
            string userName = User.Identity.Name;
            int orgid = _commonHelper.GetOrgID(HttpContext);         
            var Product_Details = await _ProductDetailPageService.GetProductDetails(id, orgid, userName);
            if(userName!=null)
            {
                Product_Details.Wishlists = await _productWishListService.GetWishList(userName, OrgId);
            }
            return View(Product_Details);
        }
        [HttpPost]
        public async Task<ActionResult> Product_Spcification_Details([FromBody]GetSpecModel get )
        {
            Guid guid = Guid.Parse(get.Guid);
            int orgid = Convert.ToInt32(get.Orgid);
            int SpecId = Convert.ToInt32(get.SpecId);

            decimal price = 0;
            var Product_Varients = await _ProductDetailPageService.GetProductVarient(guid, orgid,SpecId);
            try
            {
               foreach(var Spec in Product_Varients)
               {
                   get.Image = Spec.SpecificationImage;
                   get.name = Spec.SpecificationNames;
                   price = Spec.Price;
               }
            }
            catch (Exception e) 
            {

                throw e;
            }    

            get.Price = price;

            if(price == 0)
            {
                return Json("nothing");
            }

            return Json(get);
        }
        public async Task<IActionResult> AddToWhishList(int ProductId,Guid id)
        {
            int OrgId = _commonHelper.GetOrgID(HttpContext);
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

        public async Task<IActionResult>BuyNow(ProductDetailModel productDetailModel)
        {
           var Buy =  await _ProductDetailPageService.BuyNow(productDetailModel);
            return RedirectToAction("CheckOut", "Cart", new { orderid = Buy.OrderBasicModel.OrderGuid});
        }

    }
}
