﻿using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Models;
using Shoppite.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Helpers
{
    public class FooterDetailsViewComponent:ViewComponent
    {
        private readonly ICartPageServices _CartPageServices;
        private readonly CommonHelper _commonHelper = new CommonHelper();

        List<CartModel> cartModel = new List<CartModel>();

        public FooterDetailsViewComponent(ICartPageServices CartPageServices)
        {
             // cartModel = cartModel;
            _CartPageServices = CartPageServices;
           // _commonHelper = commonHelper;
        }

        [HttpPost]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int orgid = _commonHelper.GetOrgID(HttpContext);
            var FooterDetail = await _CartPageServices.GetVendorDetails(orgid);
           // var model = cartModel;
            return View("FooterDetails", FooterDetail);
        }
    }
}
