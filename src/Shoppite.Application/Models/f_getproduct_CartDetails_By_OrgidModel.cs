﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Application.Models
{
  public partial class f_getproduct_CartDetails_By_OrgidModel
    {
        public int? ProductId { get; set; }
        public Guid? OrderGuid { get; set; }
        public string ProductName { get; set; }
        public int? ProfileId { get; set; }
        public string UserName { get; set; }
        public Guid? ProductGuid { get; set; }
        public string CoverImage { get; set; }
        public decimal? Price { get; set; }
        public int? Qty { get; set; }
        public int? BasicQty { get; set; }
    }
}
