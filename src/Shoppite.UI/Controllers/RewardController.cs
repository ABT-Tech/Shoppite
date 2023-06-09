using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Models;
using Shoppite.UI.Interfaces;
using System.Threading.Tasks;
namespace Shoppite.UI.Controllers
{
    public class RewardController : Controller
    {
        private readonly ICommonHelper _commonHelper;
        private readonly IRewardPageService _rewardPageService;
        public RewardController(IRewardPageService rewardPageService, ICommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
            _rewardPageService = rewardPageService;
        }             
        public async Task<IActionResult> Coin_balance()
        {
            int OrgId = _commonHelper.GetOrgID(HttpContext);
            Reward_Point_LogModel rewards = new Reward_Point_LogModel();
            rewards.Reward_PointList = await _rewardPageService.GetRewardBalance(OrgId);
            return View(rewards);
        }     
    }
}
