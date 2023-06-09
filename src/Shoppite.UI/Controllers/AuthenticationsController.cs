using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Models;
using Shoppite.UI.Helpers;
using Shoppite.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shoppite.UI.Controllers
{
    [DataContract]
    public class AuthenticationsController : BaseController
    {
        private readonly IAuthenticationsPageService _AuthenticationPageService;
        private readonly ICommonHelper _commonHelper;

        public AuthenticationsController(IAuthenticationsPageService AuthenticationPageService, ICommonHelper commonHelper)
        {
            _AuthenticationPageService = AuthenticationPageService ?? throw new ArgumentNullException(nameof(AuthenticationPageService));
            _commonHelper = commonHelper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
           int orgid = _commonHelper.GetOrgID(HttpContext);
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
        public async Task<ActionResult> Login(UsersModal usersModal)
        {
            int OrgId = _commonHelper.GetOrgID(HttpContext);
            var UserValidate = await _AuthenticationPageService.Get_Login_Data(usersModal.Email,usersModal.Password, OrgId);
            if (UserValidate.Password != null && UserValidate.Email != null)
            {
                await CreateAuthenticationTicket(UserValidate);
                return RedirectToAction("Index","Home");
            }
            else
            {
               TempData["LoginValidError"] = "Username or Password is Incorrect";
            }
             return View(usersModal);
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> Register(UsersModal usersModal)
        {
            int OrgId = _commonHelper.GetOrgID(HttpContext);
          var Register = await _AuthenticationPageService.RegisterDetail(usersModal.Username,usersModal.Password,usersModal.Email,OrgId);
            if (Register == null)
                return RedirectToAction("Login");
            else
                TempData["SignValidError"] = "Email is already exsist";

            return View(usersModal);
        }

        public async Task<IActionResult> ForgotPassWord()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> ForgotPassWord(UsersModal usersModal)
        {
            if(usersModal.Password == usersModal.ConfPassword)
            {
                int OrgId = _commonHelper.GetOrgID(HttpContext);
                var ForgotPass = await _AuthenticationPageService.ForgotPass(usersModal.Password, usersModal.Email, OrgId);

                if (ForgotPass == null)
                    TempData["EmailValidError"] = "Email not found";
                else
                    return RedirectToAction("Login");
            }
            else
            {
                TempData["PassValidError"] = "Password and Confirm Password not match.";
            }
            
            return View();
        }
    }
}
