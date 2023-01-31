using Shoppite.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Shoppite.Application.Models
{
    public class CategoryMasterModel
    {
        public int CategoryId { get; set; }
        public string maincaturlpath { get; set; }
        public int AdId { get; set; }
        public string PlacementName { get; set; }
        public int AdsPlacementId { get; set; }
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public string Logo1 { get; set; }
        public string Favicon { get; set; }
        public string Urlpath { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Banner { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsShowHomePage { get; set; }
        public bool? IsIncludeMenu { get; set; }
        public string SeoPageName { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeyword { get; set; }
        public string SeoDescription { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string UserName { get; set; }
        public int? DisplayOrder { get; set; }
        public int? OrgId { get; set; }
        public string Image { get; set; }
        public string ProductName { get; set; }
        public string CoverImage { get; set; }
        public List<CategoryMasterModel> TopBanner { get; set; }
        public List<CategoryMasterModel> BottomBanner { get; set; }
        public IEnumerable<MainCategoryModel> ProductsDetails { get; set; }
        public List<CategoryMasterModel> Categories { get; set; }
        public List<CategoryMasterModel> HorizontalBanner { get; set; }
    }
    public class MainCategoryModel
    {
        public string maincaturlpath { get; set; }
        public Nullable<int> maincatid { get; set; }
        public int? CategoryId { get; set; }
        public List<CategoryProductModel> SubcategoryDetails { get; set; }
    }
    public class CategoryProductModel
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        public List<ProductBaseModel> ProductsDetails { get; set; }
    }
    public class ProductBaseModel
    {
        public int? ProductId { get; set; }
        public Guid? ProductGuid { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string CoverImage { get; set; }
    }

}
