﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Application.Models
{
    public class SP_GetProductSpecificationsModel
    {
        public string SpecificationNames { get; set; }
        public int SpecificationIds { get; set; }
        public int IsSpecificationExist { get; set; }
    }
}
