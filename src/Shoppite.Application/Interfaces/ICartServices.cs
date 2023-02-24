using Shoppite.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Interfaces
{
    public interface ICartServices
    {
        Task<CartModel> Orderbasic(int orgid);
        Task<CartModel> Delete(int id);
    }
}
