using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Application.Models
{
    public class SP_GetCategoryWiseProductCount_Model
    {
        public int ProductCount { get; set; }
        public int Category_Id { get; set; }
        public string CategoryName { get; set; }
    }
}
