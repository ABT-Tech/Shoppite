﻿using System;
namespace Shoppite.Application.Models
{
    public class Customer_WishlistModel
    {
        public int WishlistId { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public decimal? OldPrice { get; set; }
        public string CoverImage { get; set; }
        public string UserName { get; set; }
        public DateTime? InsertDate { get; set; }
        public string Ip { get; set; }
        public int? OrgId { get; set; }
    }
}
