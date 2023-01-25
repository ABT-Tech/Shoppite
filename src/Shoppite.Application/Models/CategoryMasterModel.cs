using Shoppite.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shoppite.Application.Models
{
    public class CategoryMasterModel
    {
        public int CategoryId { get; set; }
        public int AdId { get; set; }
        public string PlacementName { get; set; }
        public int AdsPlacementId { get; set; }
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public string Logo1 { get; set; }
        public string Urlpath { get; set; }
        public int ParentCategoryId { get; set; }
        public string Image { get; set; }
        public string ProductName { get; set; }
        public string CoverImage { get; set; }
        public List<CategoryMasterModel> topBanner { get; set; }
        public List<CategoryMasterModel> BottomBanner { get; set; }
        public IEnumerable<CategoryProductModel> ProductsDetails { get; set; }
        public List<CategoryMasterModel> Categories { get; set; }
        public List<CategoryMasterModel> HorizontalBanner { get; set; }
    }
    public class CategoryProductModel
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductBaseModel> ProductsDetails { get; set; }
    }
    public class ProductBaseModel
    {
        public int? ProductId { get; set; }
        public Guid? ProductGuid { get; set; }
        public string ProductName { get; set; }
        public string CoverImage { get; set; }
    }

}
