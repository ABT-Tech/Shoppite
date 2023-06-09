using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Helpers
{ 
    public class EaseBuzzRequest
    {
        public string key { get; set; }
        public string txnid{ get; set; }
        public decimal? amount { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string hash { get; set; }
    }
}
