using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Models;
using Shoppite.UI.Helpers;
using Shoppite.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Controllers
{
    public class AuthenticationsController : Controller
    {
        private readonly IAuthenticationsPageService _AuthenticationPageService;
        private readonly CommonHelper commonHelper = new CommonHelper();
        private readonly BaseController baseController = new BaseController();
        private readonly IHostingEnvironment _hostingEnvironment;

        public AuthenticationsController(IAuthenticationsPageService AuthenticationPageService, IHostingEnvironment hostingEnvironment)
        {
            _AuthenticationPageService = AuthenticationPageService ?? throw new ArgumentNullException(nameof(AuthenticationPageService));
            _hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(int orgid)
        {
            orgid = commonHelper.GetOrgID(HttpContext);
            var logo = await _AuthenticationPageService.Get_Logo(orgid);
            return View(logo);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpPost, AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody]LoginCheckModel loginCheckModel)
        {
            //usersModal.OrgId = commonHelper.GetOrgID(HttpContext);
            //var UserValidate = await _AuthenticationPageService.Get_Login_Data(usersModal.Username, usersModal.Password, (int)usersModal.OrgId);
            //if (UserValidate.Password != null && UserValidate.Username != null)
            //{
            //    _ = baseController.CreateAuthenticationTicket(UserValidate);
            //    return RedirectToAction("Index", "Home", new { area = "" });
            //}
            //else
            //{
            //    ViewBag.LoginValidError = "Username or Password is Incorrect";
            //}
            //return View(UserValidate);
            return null;
        }
    }
}
