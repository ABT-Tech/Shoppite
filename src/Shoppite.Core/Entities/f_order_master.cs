using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Core.Entities
{
    public partial class f_order_master
    {
        public int OrderId { get; set; }
        public Nullable<System.Guid> OrderGUID { get; set; }
        public string orderids { get; set; }
        public Nullable<decimal> totalPrice { get; set; }
        public string orderdate { get; set; }
        public string CurrencyName { get; set; }
        public string ProductName { get; set; }
        public string CoverImage { get; set; }
        public string UserName { get; set; }
        public Nullable<decimal> ExcludeVatPrice { get; set; }
        public Nullable<decimal> Price { get; set; }
        public int? Qty { get; set; }
        public string LastOrderStatus { get; set; }
        public string OrderStatus { get; set; }
        public Nullable<decimal> vat { get; set; }
        public Nullable<decimal> tax { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Contactnumber { get; set; }
        public string PaymentMode { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string ReferenceID { get; set; }
        public Nullable<decimal> commission { get; set; }
        public Nullable<decimal> donation { get; set; }
        public Nullable<decimal> totaaftercomanddon { get; set; }
        public Nullable<decimal> deliveryfees { get; set; }
    }
}
