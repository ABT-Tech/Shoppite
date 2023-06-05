using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Shoppite.Application.Models
{
    public class OrderShippingModel
    {
        public int ShippingId { get; set; }
        public Guid? OrderGuid { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string Phone { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Contactnumber { get; set; }
        public string OrgName { get; set; }
        public int? OrgId { get; set; }
    }
}
