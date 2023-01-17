using Shoppite.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Interfaces
{
    public interface IBrandServices
    {
       public Task<MainModel> GetBrands(int orgid);
    }
}
