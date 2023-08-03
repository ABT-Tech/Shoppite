using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Core.Entities
{
    public class SP_GetCategoryWiseProductCount
    {
        public int ProductCount { get; set; }
        public int Category_Id { get; set; }
        public string CategoryName { get; set; }
    }
}
