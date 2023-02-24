﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Application.Models
{
    public class F_Orders_All_Model
    {
        public int OrderId { get; set; }
        public Guid? OrderGuid { get; set; }
        public string ProductName { get; set; }
        public string CoverImage { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal? Tax { get; set; }
        public decimal? DeliveryFees { get; set; }
        public decimal? Vat { get; set; }
        public string Comments { get; set; }
        //public string UserName { get; set; }
        public DateTime? InsertDate { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentMode { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string ReferenceId { get; set; }
        public decimal? Commission { get; set; }
        public decimal? Donation { get; set; }
        public string orderdeliverystatus { get; set; }
        public int ProfileId { get; set; }
    }
}
