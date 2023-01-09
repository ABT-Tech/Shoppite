using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Shoppite.Core.Entities
{
    public partial class OrderMaster
    {
        public Guid OrderGuid { get; set; }
        public DateTime? InsertDate { get; set; }
        public string OrderKeyStatus { get; set; }
        public int? OrgId { get; set; }
    }
}
