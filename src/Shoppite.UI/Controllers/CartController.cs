namespace Shoppite.UI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Shoppite.Application.Models;
    using Shoppite.UI.Extensions;
    using Shoppite.UI.Interfaces;
    using Shoppite.Web.Interfaces;
    using System;
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
        /// Initializes a new instance of the <see cref="CartController"/> class.
        /// </summary>
        /// <param name="cartPageServices">The cartPageServices<see cref="ICartPageServices"/>.</param>
        /// <param name="commonHelper">The commonHelper<see cref="ICommonHelper"/>.</param>
        /// 
        private readonly IRewardPageService _rewardPageService;
        private readonly IWishlistPageService _wishlistpageService;
        public CartController(IWishlistPageService wishlistPageService,ICartPageServices cartPageServices, ICommonHelper commonHelper,IRewardPageService rewardPageService)
        {
            _cartPageService = cartPageServices ?? throw new ArgumentNullException(nameof(cartPageServices));
            _commonHelper = commonHelper;
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
            //  var orderId = Request.Query
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
            Model.MyOrderDetails = await _wishlistpageService.GetMyOrders(User.Identity.Name, orgid);
            Model.reward_Point_Log = new Reward_Point_LogModel();
            Model.reward_Point_Log.OrgId = orgid;
            if (Model.MyOrderDetails.Count == 0)
            {
                await _rewardPageService.AddRewards(Model.reward_Point_Log);
            }
            Model.OrderShippingModel.OrgId = orgid;
            await _cartPageService.SaveAddress(Model);

            await _cartPageService.UpdateOrder((Guid)Model.OrderBasicModel.OrderGuid);            

            return RedirectToAction("OrderSuccess");
        }
        [HttpPost]
        public async Task<IActionResult> SaveAddress2(CartModel cartModel)
        {
            int id = _commonHelper.GetOrgID(HttpContext);
            cartModel.OrderShippingModel.OrgId = id;
            await _cartPageService.SaveAddress(cartModel);

            await _cartPageService.UpdateOrder((Guid)cartModel.OrderBasicModel.OrderGuid);
            var rewardpointBalance = 0.0M;
            cartModel.myreward = await _rewardPageService.GetRewardBalance(id);
            cartModel.reward_Point_Log = new Reward_Point_LogModel();
            cartModel.reward_Point_Log.OrgId = id;
            if (cartModel.myreward != null)
            {
                foreach (var rewardpoint in cartModel.myreward)
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
            if(rewardpointBalance!=0)
            {
                cartModel.reward_Point_Log.Reward_points = (rewardpointBalance * 30) / 100;
                await _rewardPageService.ClaimReward(cartModel.reward_Point_Log);

            }
            return RedirectToAction("OrderSuccess");
        }
    }
}
