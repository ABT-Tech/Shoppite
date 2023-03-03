using Shoppite.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Interfaces
{
   public interface ICartPageServices
    {
        Task<CartModel>OrderBasic(int orgid);
        Task<CartModel> DeleteAysnc(int id);
        Task SaveAddress(CartModel cartModel);
        Task UpdateOrder(CheckOutModel checkOut);
    }
}
