using Shoppite.Application.Models;
using Shoppite.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;

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
            CreateMap<CategoryMasterModel, CategoryMaster>().ReverseMap();
            CreateMap<CategoryMasterModel, AdsDetail>().ReverseMap();
            CreateMap<CategoryMasterModel, ProductBasic>().ReverseMap();
            CreateMap<CategoryMasterModel, f_All_getcat_Result>().ReverseMap();
            CreateMap<CategoryMasterModel, f_getproducts_By_CategoryID_Result>().ReverseMap();
            CreateMap<CategoryMasterModel, Logo>().ReverseMap();

        }
    }
}
