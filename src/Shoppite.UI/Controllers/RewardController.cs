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
        private readonly ILogger<WishlistController> _logger;
        private readonly IBrandPageServices _BrandPageService;
        private readonly ICategoryPageService _categoryPageService;
        private readonly IRewardPageService _rewardPageService;
        private readonly IMyAccountPageService _myAccountPage;
        public RewardController(IRewardPageService rewardPageService,IMyAccountPageService myAccountPageService,IBrandPageServices brandPageServices, ICategoryPageService categoryPageService, ILogger<WishlistController> logger, IWishlistPageService productPageService, IHttpContextAccessor accessor, ICommonHelper commonHelper)
        {
            _accessor = accessor;
            _BrandPageService = brandPageServices ?? throw new ArgumentNullException(nameof(brandPageServices));
            _categoryPageService = categoryPageService ?? throw new ArgumentNullException(nameof(categoryPageService));
            _logger = logger ?? throw new ArgumentNullException();
            _commonHelper = commonHelper;
            _rewardPageService = rewardPageService;
            _productWishListService = productPageService ?? throw new ArgumentNullException(nameof(productPageService));
            _myAccountPage = myAccountPageService ?? throw new ArgumentNullException(nameof(productPageService));
        }
        public async Task<IActionResult> Rewards()
        {
            int OrgId = _commonHelper.GetOrgID(HttpContext);
            Reward_Point_LogModel rewards = new Reward_Point_LogModel();
            //  string userName = User.Identity.Name;
            //  int Profileid = await _myAccountPage.GetProfileId(User.Identity.Name, OrgId);
            rewards.Reward_PointList = await _rewardPageService.GetRewardBalance(OrgId, 4);
            return View(rewards);
        }
        [HttpGet]
        public  IActionResult Claim_reward()
        {            
            return View();
        }
        public async Task<IActionResult> Coin_balance()
        {
            int OrgId = _commonHelper.GetOrgID(HttpContext);
            Reward_Point_LogModel rewards = new Reward_Point_LogModel();
            rewards.Reward_PointList = await _rewardPageService.GetRewardBalance(OrgId, 4);
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
