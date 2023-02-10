using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Application.Models
{
   public class MainModel
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<BrandsModel> BrandsModel { get; set; }
        public List<f_getproducts_By_NewArrivalsModel> ProductNewArrivalModel { get; set; }
        public List<f_getproducts_By_OrgIDModel> f_Getproducts_By_OrgIDModel { get; set; }
        public List<sp_getcat_ResultModel> GetSp_Getcat_ResultModel { get; set; }
        public List<f_getproducts_By_CategoryIDModel> F_Getproducts_By_CategoryIDModels { get; set; }
        public List<CategoryMasterModel> CategoryMasterModel { get; set; }
        public CategoryMasterModel CategoryMaster { get; set; }
        public List<CategoryMasterModel> TopBanner { get; set; }
        public List<f_getproducts_By_CatID_SpecificationNameModel> product_sepecification { get; set; }
        public List<CategoryMasterModel> MiddelBanner { get; set; }
        public IEnumerable<MainCategoryModel> ProductsDetails { get; set; }
        public List<SpecificationSetupModel> Specification { get; set; }
        public List<CategoryMasterModel> CategoryProduct { get; set; }
        public List<CategoryMasterModel> Categories { get; set; }
        public List<CategoryMasterModel> SubCategories { get; set; }
        public List<CategoryMasterModel> HorizontalBanner { get; set; }
        public List<AttributeSetupModel> Attributes { get; set; }
    }
}
