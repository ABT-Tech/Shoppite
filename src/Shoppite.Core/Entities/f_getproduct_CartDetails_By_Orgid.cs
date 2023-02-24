using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Core.Entities
{
  public partial class f_getproduct_CartDetails_By_Orgid
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid? ProductGuid { get; set; }
        public string CoverImage { get; set; }
        public decimal? Price { get; set; }
        public int? Qty { get; set; }
    }
}
