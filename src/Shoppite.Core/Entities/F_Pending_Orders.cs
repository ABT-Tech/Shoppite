﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Core.Entities
{
    public class F_Pending_Orders
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public string CoverImage { get; set; }
        public string OrderStatus { get; set; }
        public int OrgId { get; set; }
        //public int ProductId { get; set; }
        public int ProfileId { get; set; }
        public DateTime? InsertDate { get; set; }
       // public string OrderVariation { get; set; }
    }
}
