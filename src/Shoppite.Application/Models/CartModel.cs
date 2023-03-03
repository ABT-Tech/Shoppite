﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Shoppite.Application.Models
{ 
    public class CartModel
    {
      public  List<OrderBasicModel> OrderBasicModels { get; set; }
      public  List<f_getproduct_CartDetails_By_OrgidModel> F_Getproduct_CartDetails_By_Orgids { get; set; }
        public OrderShippingModel OrderShippingModel { get; set; }
    }
}
