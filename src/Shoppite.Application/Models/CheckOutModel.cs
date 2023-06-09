using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Application.Models
{
   public class CheckOutModel
    {
        public decimal total { get; set; }
        public List<CheckOutOrder> ar { get; set; }
    }

    public class CheckOutOrder
    {
        public string Guid { get; set; }
        public string Qty { get; set; }
        public string ProductId { get; set; }
    }

}
