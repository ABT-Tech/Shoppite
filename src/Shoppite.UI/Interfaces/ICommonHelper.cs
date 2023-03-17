using Microsoft.AspNetCore.Http;
using Shoppite.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Interfaces
{
   public interface ICommonHelper
    {
        int GetOrgID(HttpContext httpContext);
    }
}
