using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shoppite.Application.Models
{
   public class MainModel
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        public string CPassword { get; set; }
        public string Address { get; set; }
        public string OrderStatus { get; set; }
        public int WishlistId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string CoverImage { get; set; }
        public string UserName { get; set; }
        public string Username { get; set; }
        public DateTime? InsertDate { get; set; }
        public string Ip { get; set; }
        public int? OrgId { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string category_name { get; set; }
        public string Banner { get; set; }
        public List<BrandsModel> BrandsModel { get; set; }
        public List<f_getproducts_By_NewArrivalsModel> ProductNewArrivalModel { get; set; }
        public List<f_getproducts_By_OrgIDModel> f_Getproducts_By_OrgIDModel { get; set; }
        public List<sp_getcat_ResultModel> GetSp_Getcat_ResultModel { get; set; }
        public List<f_getproducts_By_CategoryIDModel> F_Getproducts_By_CategoryIDModels { get; set; }
        public List<CategoryMasterModel> CategoryMasterModel { get; set; }
        public CategoryMasterModel CategoryMaster { get; set; }
        public List<CategoryMasterModel> TopBanner { get; set; }
        public List<CategoryMasterModel> MiddelBanner { get; set; }
        public IEnumerable<MainCategoryModel> ProductsDetails { get; set; }
        public List<SpecificationSetupModel> Specification { get; set; }
        public List<CategoryMasterModel> CategoryProduct { get; set; }
        public List<CategoryMasterModel> Categories { get; set; }
        public List<CategoryMasterModel> SubCategories { get; set; }
        public List<CategoryMasterModel> HorizontalBanner { get; set; }
        public List<AttributeSetupModel> Attributes { get; set; }
        public List<f_getproducts_By_CatID_SpecificationNameModel> Product_specification { get; set; }
        public List<Customer_WishlistModel> Wishlists { get; set; }
        public List<F_Orders_All_Model> MyOrders { get; set; }
        public List<F_Pending_Orders_Model> Orders { get; set; }
        public List<f_Get_MyAccount_Data_Model> Myaccount { get; set; }
        public List<sp_getcat_ResultModel> AllCategories { get; set; }
    }
}
