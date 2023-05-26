using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shoppite.Application.Models;
using Shoppite.UI.Interfaces;
using Shoppite.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Controllers
{
    public class RewardController : Controller
    {
        private readonly ICommonHelper _commonHelper;
        private readonly IWishlistPageService _productWishListService;
        private IHttpContextAccessor _accessor;
        private readonly ICategoryPageService _categoryPageService;
        private readonly IRewardPageService _rewardPageService;
        private readonly IMyAccountPageService _myAccountPage;
        public RewardController(IRewardPageService rewardPageService,ILogger<WishlistController> logger, IHttpContextAccessor accessor, ICommonHelper commonHelper,IWishlistPageService pageservice)
        {
            _accessor = accessor;
            _productWishListService = pageservice;
            _commonHelper = commonHelper;
            _rewardPageService = rewardPageService;
        }
        [HttpGet]
        public async Task<IActionResult> Rewards()
        {
            int OrgId = _commonHelper.GetOrgID(HttpContext);
            Reward_Point_LogModel rewards = new Reward_Point_LogModel();
            rewards.Reward_PointList = await _rewardPageService.GetRewardBalance(OrgId);
            return View(rewards);
        }

        [HttpPost]
        public async Task<IActionResult> Rewards(Reward_Point_LogModel reward)
        {
            int OrgId = _commonHelper.GetOrgID(HttpContext);
            reward.MyOrderDetails = await _productWishListService.GetMyOrders(User.Identity.Name, OrgId);        
            reward.OrgId = OrgId;
            if(reward.MyOrderDetails.Count==0)
            {
                await _rewardPageService.AddRewards(reward);

            }
            
            return View(reward);

        }
        [HttpGet]
        public async Task<IActionResult> Claim_reward()
        {
            int OrgId = _commonHelper.GetOrgID(HttpContext);
            Reward_Point_LogModel rewards = new Reward_Point_LogModel
            {
                Reward_PointList = await _rewardPageService.GetRewardBalance(OrgId)
            };
            return View(rewards);
        }
        public async Task<IActionResult> Coin_balance()
        {
            int OrgId = _commonHelper.GetOrgID(HttpContext);
            Reward_Point_LogModel rewards = new Reward_Point_LogModel();
            rewards.Reward_PointList = await _rewardPageService.GetRewardBalance(OrgId);
            return View(rewards);
        }
        public IActionResult Bank_Coupan()
        {
            return View();
        }
        public IActionResult Reward_store()
        {
            return View();
        }
    }
}
