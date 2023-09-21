using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Application.Models
{
   public class ParentCategoryWiseSubCategoryModel
    {       
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
