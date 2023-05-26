using Microsoft.AspNetCore.Mvc;
using Shoppite.Application.Models;
using Shoppite.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Helpers
{
    public class ChatBotViewComponent: ViewComponent
    {
        MessagesModel messagesModel = new MessagesModel();
        private readonly ICommonHelper _commonHelper;
        public ChatBotViewComponent(ICommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
        }
        [HttpPost]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("ChatBot",messagesModel);
        }
    }
}
