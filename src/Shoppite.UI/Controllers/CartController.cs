namespace Shoppite.UI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Shoppite.Application.Models;
    using Shoppite.UI.Extensions;
    using Shoppite.UI.Helpers;
    using Shoppite.UI.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CartController" />.
    /// </summary>
    [Authorize]
    public class CartController : Controller
    {
        /// <summary>
        /// Defines the _cartPageService.
        /// </summary>
        private readonly ICartPageServices _cartPageService;

        /// <summary>
        /// Defines the _commonHelper.
        /// </summary>
        private readonly ICommonHelper _commonHelper;

        /// <summary>
        /// Defines the _commonHelper.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartController"/> class.
        /// </summary>
        /// <param name="cartPageServices">The cartPageServices<see cref="ICartPageServices"/>.</param>
        /// <param name="commonHelper">The commonHelper<see cref="ICommonHelper"/>.</param>
        public CartController(ICartPageServices cartPageServices, ICommonHelper commonHelper, IConfiguration configuration)
        {
            _cartPageService = cartPageServices ?? throw new ArgumentNullException(nameof(cartPageServices));
            _commonHelper = commonHelper;
            _configuration = configuration;
        }

        /// <summary>
        /// The Cart.
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        public async Task<IActionResult> Cart()
        {
            int orgid = _commonHelper.GetOrgID(HttpContext);
            var cartlist = await _cartPageService.OrderBasic(orgid);
            foreach (var swap in cartlist.F_Getproduct_CartDetails_By_Orgids)
            {
                if (swap.Qty > swap.BasicQty)
                {
                    swap.Qty = swap.BasicQty;
                }
            }

            return View(cartlist);
        }

        /// <summary>
        /// The DeleteProduct.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            _cartPageService.DeleteAysnc(id);
            return RedirectToAction("Cart");
        }

        /// <summary>
        /// The AddToCheck.
        /// </summary>
        /// <param name="checkOut">The checkOut<see cref="CheckOutModel"/>.</param>
        /// <returns>The <see cref="Task{ActionResult}"/>.</returns>
        [HttpPost]
        public async Task<ActionResult> AddToCheck([FromBody] CheckOutModel checkOut)
        {
            await _cartPageService.UpdateOrderQty(checkOut);

            return Json(checkOut);
        }

        /// <summary>
        /// The CheckOut.
        /// </summary>
        /// <param name="orderid">The orderid<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ActionResult}"/>.</returns>
        public async Task<ActionResult> CheckOut(Guid orderid)
        {
            var order = await _cartPageService.CheckOrder(orderid);
            var merchantDetails = _commonHelper.GetMerchantDetails(HttpContext);
            order.IsPaytm = false;
            if (merchantDetails != null)
            {
                order.IsPaytm = true;
            }
            return View(order);
        }

        /// <summary>
        /// The OrderSuccess.
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        public async Task<IActionResult> OrderSuccess()
        {
            // await _cartPageService.UpdateOrder(orderid);Guid orderid
            return View();
        }

        /// <summary>
        /// The SaveAddress.
        /// </summary>
        /// <param name="Model">The Model<see cref="CartModel"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> SaveAddress(CartModel Model)
        {
            int orgid = _commonHelper.GetOrgID(HttpContext);
            Model.OrderShippingModel.OrgId = orgid;
            await _cartPageService.SaveAddress(Model);

            await _cartPageService.UpdateOrder((Guid)Model.OrderBasicModel.OrderGuid);

            return RedirectToAction("OrderSuccess");
        }

        [HttpPost]
        public async Task<IActionResult> MakePaymentRequest(CartModel Model)
        {
            if (Model.IsPaytm)
            {
                var order = await _cartPageService.CheckOrder((Guid)Model.OrderBasicModel.OrderGuid);
                var merchantDetails = _commonHelper.GetMerchantDetails(HttpContext);
                var strProductMapping = string.Empty;
                decimal? TotalOrderCharge = 0;
                decimal? TotalProductCharges = 0;
                var IsTestEnable = _configuration.GetSection("OnePeSettings")["IsTest"].ToString();
                ViewBag.AggregatorRedirectionLink = _configuration.GetSection("OnePeSettings")["URL"];
                foreach (var orderDetails in order.OrderBasicModels)
                {
                    TotalProductCharges = orderDetails.Price + orderDetails.DeliveryFees;
                    if (string.IsNullOrEmpty(strProductMapping))
                        strProductMapping += orderDetails.ProductId + "~" + TotalProductCharges;
                    else
                        strProductMapping += "|"+orderDetails.ProductId + "~" + TotalProductCharges;
                    TotalOrderCharge = TotalOrderCharge + TotalProductCharges;
                }
                using (HttpClient client = new HttpClient())
                {
                    MerchantParams merchantParams = new MerchantParams();
                    merchantParams.merchantId = merchantDetails.AggregatorMerchantId;
                    merchantParams.apiKey = merchantDetails.AggregatorMerchantApiKey;
                    merchantParams.txnId = order.OrderBasicModel.OrderGuid.ToString();
                    merchantParams.amount = IsTestEnable == "1"?"10.00": TotalOrderCharge.ToString();
                    merchantParams.dateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    merchantParams.custMail = Model.OrderShippingModel.Email;
                    merchantParams.custMobile = Model.OrderShippingModel.Phone;
                    merchantParams.udf1 = Model.OrderShippingModel.Address;
                    merchantParams.udf2 = Model.OrderShippingModel.Address;
                    merchantParams.returnURL = merchantDetails.AggregatorCallbackURL+ "/Cart/OrderSuccess";
                    merchantParams.isMultiSettlement =  "0";
                    merchantParams.productId = "DEFAULT";
                    merchantParams.channelId = "0";
                    merchantParams.txnType = "DIRECT";
                    merchantParams.Rid = merchantDetails.AggregatorRID.ToString();
                    var objMerchantParams = JsonConvert.SerializeObject(merchantParams);
                    string encryptedParams = EncryptPaymentRequest(merchantDetails.AggregatorMerchantId, merchantDetails.AggregatorMerchantApiKey, objMerchantParams);
                    ViewBag.merchantId = merchantDetails.AggregatorMerchantId;
                    ViewBag.reqData = encryptedParams;
                }
                int orgid = _commonHelper.GetOrgID(HttpContext);
                Model.OrderShippingModel.OrgId = orgid;
                await _cartPageService.SaveAddress(Model);
                await _cartPageService.UpdateOrder((Guid)Model.OrderBasicModel.OrderGuid);
                return View();
            }
            else
            {
                int orgid = _commonHelper.GetOrgID(HttpContext);
                Model.OrderShippingModel.OrgId = orgid;
                await _cartPageService.SaveAddress(Model);
                await _cartPageService.UpdateOrder((Guid)Model.OrderBasicModel.OrderGuid);
                return RedirectToAction("OrderSuccess");
            }
            
        }

        public string EncryptPaymentRequest(string merchantId,string key,string merchantParamsJson)
        {
            String encryptedText = string.Empty;
            try
            {
                string original = merchantParamsJson; 
                string merchantEncryptionKey = key.Substring(0, 16);
                encryptedText = _commonHelper.EncryptAES256_V3(merchantParamsJson, key, merchantEncryptionKey);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }

            return encryptedText;
        }
        
    }
}
