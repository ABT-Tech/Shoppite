using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Controllers
{
    public class RewardController : Controller
    {
        public IActionResult Rewards()
        {
            return View();
        }
        [HttpGet]
        public  IActionResult Claim_reward()
        {            
            return View();
        }
        public IActionResult Coin_balance()
        {
            return View();
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
