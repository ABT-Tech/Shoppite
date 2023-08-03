using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Models;
using Shoppite.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Helpers
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(Responsedata result)
        {
            return Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/PagerView/Default.cshtml", result));
        }
    }
}
