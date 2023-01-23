using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Application.Models
{
    public class AdsDetailModel
    {
        public int AdId { get; set; }
        public int? AdsPlacementId { get; set; }
        public int? AdsPageId { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int OrgId { get; set; }
    }
}
