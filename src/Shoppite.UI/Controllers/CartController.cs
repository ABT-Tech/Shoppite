namespace Shoppite.UI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Shoppite.Application.Models;
    using Shoppite.UI.Extensions;
    using Shoppite.UI.Helpers;
    using Shoppite.UI.Interfaces;
    using Shoppite.Web.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.IO;
    using System.Net.Http;
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
       
        private readonly IRewardPageService _rewardPageService;
        private readonly IWishlistPageService _wishlistpageService;
        public CartController(IWishlistPageService wishlistPageService,ICartPageServices cartPageServices, ICommonHelper commonHelper,IRewardPageService rewardPageService, IConfiguration configuration)
        {
            _cartPageService = cartPageServices ?? throw new ArgumentNullException(nameof(cartPageServices));
            _commonHelper = commonHelper;
            _configuration = configuration;
            _rewardPageService = rewardPageService;
            _wishlistpageService = wishlistPageService;
        }

        /// <summary>
        /// The Cart.
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        public async Task<IActionResult> Cart()
        {
            int orgid = _commonHelper.GetOrgID(HttpContext);
            var cartlist = await _cartPageService.OrderBasic(orgid);
            cartlist.myreward = await _rewardPageService.GetRewardBalance(orgid);
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
            return View(order);
        }

        /// <summary>
        /// The OrderSuccess.
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        public async Task<IActionResult> OrderSuccess()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> MakePaymentRequest(CartModel Model)
        {
            int orgid = _commonHelper.GetOrgID(HttpContext);
            var orgname=await _cartPageService.FindOrganizationName(orgid);
            Model.myreward = await _rewardPageService.GetRewardBalance(orgid);
            var rewardpointBalance = 0.0M;
            if (Model.myreward != null)
            {
                foreach (var rewardpoint in Model.myreward)
                {
                    if (rewardpoint.Operation_type == "Credit")
                    {
                        rewardpointBalance += rewardpoint.Reward_points;
                    }
                    else
                    {
                        rewardpointBalance -= rewardpoint.Reward_points;
                    }
                }
            }
            if (Model.IsPaytm)
            {
                var order = await _cartPageService.CheckOrder((Guid)Model.OrderBasicModel.OrderGuid);
                using (HttpClient client = new HttpClient())
                {
                    CashfreeRequest cashfreeRequest = new CashfreeRequest();
                    var orderId = Guid.NewGuid();
                    cashfreeRequest.customer_details = new CustomerDetails();
                    cashfreeRequest.customer_details.customer_email = Model.OrderShippingModel.Email;
                    cashfreeRequest.customer_details.customer_id = Model.OrderShippingModel.ShippingId.ToString();
                    cashfreeRequest.customer_details.customer_phone = Model.OrderShippingModel.Phone;
                    cashfreeRequest.customer_details.OrganizationName = orgname.OrgName;
                    cashfreeRequest.order_amount = (decimal)order.OrderBasicModel.Price;
                    cashfreeRequest.order_currency = "INR";
                    cashfreeRequest.order_id = Model.OrderBasicModel.OrderGuid.ToString();
                    StringContent content = new StringContent(JsonConvert.SerializeObject(cashfreeRequest), Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Add("x-api-version", _configuration.GetSection("CashFreeSettings")["x-api-version"]);
                    client.DefaultRequestHeaders.Add("x-client-id", _configuration.GetSection("CashFreeSettings")["x-client-id"]);
                    client.DefaultRequestHeaders.Add("x-client-secret", _configuration.GetSection("CashFreeSettings")["x-client-secret"]);
                    string endpoint = _configuration.GetSection("CashFreeSettings")["URL"];
                    using (var Response = await client.PostAsync(endpoint, content))
                    {
                        var payment_session = string.Empty;
                        var stream = await Response.Content.ReadAsStringAsync();
                        var dynamicObject = JsonConvert.DeserializeObject<ExpandoObject>(stream) as dynamic;
                        if (_commonHelper.DoesPropertyExist(dynamicObject, "payment_session_id")){
                            payment_session = dynamicObject.payment_session_id;
                        }
                        ViewBag.PaymentSession = payment_session;
                    }
                }
                Model.MyOrderDetails = await _wishlistpageService.GetMyOrders(User.Identity.Name, orgid);
                Model.reward_Point_Log = new Reward_Point_LogModel();
                Model.reward_Point_Log.OrgId = orgid;
                if (Model.MyOrderDetails.Count == 0)
                {
                    await _rewardPageService.AddRewards(Model.reward_Point_Log);
                }
                //if reward is claimed
                if (Model.IsReward)
                {
                    if (rewardpointBalance != 0)
                    {
                        Model.reward_Point_Log.Reward_points = (rewardpointBalance * 30) / 100;
                        await _rewardPageService.ClaimReward(Model.reward_Point_Log);
                    }
                }

                Model.OrderShippingModel.OrgId = orgid;
                await _cartPageService.SaveAddress(Model);
                await _cartPageService.UpdateOrder((Guid)Model.OrderBasicModel.OrderGuid);
                return View();
            }
            else
            {
                Model.MyOrderDetails = await _wishlistpageService.GetMyOrders(User.Identity.Name, orgid);
                Model.reward_Point_Log = new Reward_Point_LogModel();
                Model.reward_Point_Log.OrgId = orgid;
                if (Model.MyOrderDetails.Count == 0)
                {
                    await _rewardPageService.AddRewards(Model.reward_Point_Log);
                }

                if (Model.IsReward)
                {                   
                    if (rewardpointBalance != 0)
                    {
                        Model.reward_Point_Log.Reward_points = (rewardpointBalance * 30) / 100;
                        await _rewardPageService.ClaimReward(Model.reward_Point_Log);
                    }
                }

                Model.OrderShippingModel.OrgId = orgid;
                await _cartPageService.SaveAddress(Model);
                await _cartPageService.UpdateOrder((Guid)Model.OrderBasicModel.OrderGuid);
                return RedirectToAction("OrderSuccess");
            }
            
        }
    }
}
