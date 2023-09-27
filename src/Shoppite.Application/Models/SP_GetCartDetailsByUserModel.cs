using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Application.Models
{
    public class SP_GetCartDetailsByUserModel
    {
        public int? OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CoverImage { get; set; }
        public string SpecificationName { get; set; }
        public decimal? Price { get; set; }
        public int? Qty { get; set; }
        public int? BasicQty { get; set; }
        public String AttributeName { get; set; }
        public string UserName { get; set; }
        public decimal? DeliveryFees { get; set; }
        public Boolean WishlistedProduct { get; set; }
    }
}
