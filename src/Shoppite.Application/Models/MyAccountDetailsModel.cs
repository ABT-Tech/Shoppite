using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Application.Models
{
   public class MyAccountDetailsModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AltMobile { get; set; }
        public string AddressType { get; set; }
        public bool IsDefault { get; set; }
        public int? OrgId { get; set; }
        public int? UserId { get; set; }
    }
}
