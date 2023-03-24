﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Shoppite.Application.Models
{
    public class ProductBasicModel
    {
        public int ProductId { get; set; }
        public Guid? ProductGuid { get; set; }
        public string ProductName { get; set; }
        public string Urlpath { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public int? Qty { get; set; }
        public DateTime? ProductStartDate { get; set; }
        public DateTime? ProductEndDate { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ProfileId { get; set; }
        public string CoverImage { get; set; }
        public int? OrgId { get; set; }
        public decimal? Price { get; set; }
        public decimal? OldPrice { get; set; }
        public ProductPriceModel ProductPricemodel { get; set; }
    }
}
