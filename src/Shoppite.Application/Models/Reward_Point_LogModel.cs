using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Application.Models
{
    public class Reward_Point_LogModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? OrgId { get; set; }
        public int Reward_points { get; set; }
        public string Reward_type { get; set; }
        public string Operation_type { get; set; }
        public DateTime? Date_created { get; set; }
        public DateTime? Expired_on { get; set; }
        public List<Reward_Point_LogModel> Reward_PointList { get; set; }
        public List<f_order_masterDetailsModel> MyOrderDetails { get; set; }
    }
}
