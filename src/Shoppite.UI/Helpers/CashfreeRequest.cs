using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Helpers
{
    public class CustomerDetails
    {
        public string customer_id { get; set; }
        public string customer_email { get; set; }
        public string customer_phone { get; set; }
    }

    public class CashfreeRequest
    {
        public string order_id { get; set; }
        public decimal order_amount { get; set; }
        public string order_currency { get; set; }
        public CustomerDetails customer_details { get; set; }
    }
}
