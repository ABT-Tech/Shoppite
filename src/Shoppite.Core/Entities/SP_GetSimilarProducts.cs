using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shoppite.Core.Entities
{
   public  class SP_GetSimilarProducts
    {
        public string Title { get; set; }
        //public int Quantity { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int? Id { get; set; }
        public int orgId { get; set; }
        public Guid ProductGUID { get; set; }
        [NotMapped]
        public string[] ProductList { get; set; }
      //  public Boolean WishlistedProduct { get; set; }
        public decimal OldPrice { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
    }
}
