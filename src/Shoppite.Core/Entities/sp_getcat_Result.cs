using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Core.Entities
{
    public partial class sp_getcat_Result
    {
        public Nullable<int> ID { get; set; }
        public Nullable<int> PARENT_NAMEID { get; set; }
        public Nullable<int> HLevel { get; set; }
        public string NAME { get; set; }
        public string Displayname { get; set; }
        public string catnames { get; set; }
    }
}
