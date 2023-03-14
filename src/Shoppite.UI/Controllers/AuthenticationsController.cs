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
    public class AuthenticationsController : BaseController
    {
        private readonly IAuthenticationsPageService _AuthenticationPageService;
        private readonly CommonHelper commonHelper = new CommonHelper();
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
        //[HttpPost, AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(UsersModal usersModal)
        //{
        //    usersModal.OrgId = commonHelper.GetOrgID(HttpContext);
        //    var UserValidate = await _AuthenticationPageService.Get_Login_Data(usersModal.Username, usersModal.Password, (int)usersModal.OrgId);
        //    if (UserValidate.Password != null && UserValidate.Username != null)
        //    {
        //        _ = CreateAuthenticationTicket(UserValidate);

        //        return RedirectToAction("Index", "Home", new { area = "" });
        //    }
        //    else
        //    {
        //        ViewBag.LoginValidError = "Username or Password is Incorrect";
        //    }
        //    return View(UserValidate);
        //}

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginCheckModel loginCheckModel)
        {
            int OrgId = commonHelper.GetOrgID(HttpContext);
            var UserValidate = await _AuthenticationPageService.Get_Login_Data(loginCheckModel.datal.userid,loginCheckModel.datal.password,OrgId);
            if (UserValidate.Password != null && UserValidate.Email != null)
            {
                _ = CreateAuthenticationTicket(UserValidate);

                return Json("Succsess");
            }
            else
            {
                ViewBag.LoginValidError = "Username or Password is Incorrect";
            }
            //return RedirectToAction("Index","Home", new { area = "" });
            return Json("fail");
            // return Json(loginCheckModel);
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterModel registerModel)
        {
            int OrgId = commonHelper.GetOrgID(HttpContext);
          var Register = await _AuthenticationPageService.RegisterDetail(registerModel.Regdata.UserName, registerModel.Regdata.Password, registerModel.Regdata.Email,OrgId);
            if (Register == null)
                return Json("Succsess");
            
            else
                return Json("already");
            //return RedirectToAction("Index","Home", new { area = "" });
            // return Json(loginCheckModel);
        }
        public IActionResult LogIn1()
        {
            return View();
        }
    }
}
