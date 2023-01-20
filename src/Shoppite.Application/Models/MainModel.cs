using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Application.Models
{
   public class MainModel
    {
        public List<BrandsModel> BrandsModel { get; set; }
        public List<f_getproducts_By_NewArrivalsModel> ProductNewArrivalModel { get; set; }
    }
}
