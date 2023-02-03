using Shoppite.Application.Models;
using Shoppite.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shoppite.Application.Mapper
{
    // The best implementation of AutoMapper for class libraries -> https://www.abhith.net/blog/using-automapper-in-a-net-core-class-library/
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<ShoppiteDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class ShoppiteDtoMapper : AutoMapper.Profile
    {
        public ShoppiteDtoMapper()
        {
            CreateMap<Brands, BrandsModel>().ReverseMap();
            CreateMap<List<Brands>, BrandsModel>().ReverseMap();
            CreateMap<List<f_getproducts_By_NewArrivals>, f_getproducts_By_NewArrivalsModel>().ReverseMap();
            CreateMap<f_getproducts_By_NewArrivals, f_getproducts_By_NewArrivalsModel>().ReverseMap();
            CreateMap<IQueryable<f_getproducts_By_NewArrivals>, List<f_getproducts_By_NewArrivalsModel>>().ReverseMap();
            CreateMap<f_getproducts_By_OrgID, f_getproducts_By_OrgIDModel>().ReverseMap();
            CreateMap<sp_getcat_Result, sp_getcat_ResultModel>().ReverseMap();
            CreateMap<f_getproducts_By_CategoryID, f_getproducts_By_CategoryIDModel>().ReverseMap();
            CreateMap<CategoryMaster, CategoryMasterModel>().ReverseMap();
            CreateMap<ProductBasic,ProductBasicModel>().ReverseMap();
            CreateMap<ProductBasic,ProductDetailModel>().ReverseMap();
            CreateMap<ProductImages,ProductImagesModel>().ReverseMap();
            CreateMap<ProductPrice,ProductPriceModel>().ReverseMap();
            CreateMap<Brands,BrandsModel>().ReverseMap();
            CreateMap<ProductBrand,ProductBrand>().ReverseMap();
            CreateMap<ProductCategory,ProductCategoryModel>().ReverseMap();
            CreateMap<ProductRecentlyViewed, ProductRecentlyViewedModel>().ReverseMap();
            CreateMap<ProductRecentlyViewed, ProductDetailModel>().ReverseMap();
            CreateMap<f_getproducts_Recentlyviewed,f_getproducts_RecentlyviewedModel>().ReverseMap();

            CreateMap<CategoryMasterModel, CategoryMaster>().ReverseMap();
            CreateMap<CategoryMasterModel, AdsDetail>().ReverseMap();
            CreateMap<CategoryMasterModel, ProductBasicModel>().ReverseMap();
            CreateMap<CategoryMasterModel, f_All_getcat_Result>().ReverseMap();
            CreateMap<CategoryMasterModel, f_getproducts_By_CategoryID_Result>().ReverseMap();
            CreateMap<CategoryMasterModel, Logo>().ReverseMap();

        }
    }
}
