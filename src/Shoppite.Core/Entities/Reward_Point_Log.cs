using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Core.Entities
{
    public partial class Reward_Point_Log
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? OrgId { get; set; }
        public int Reward_points { get; set; }
        public string Reward_type  { get; set; }
        public string Operation_type { get; set; }
        public DateTime? Date_created { get; set; }
        public DateTime? Expired_on { get; set; }
    }
}
