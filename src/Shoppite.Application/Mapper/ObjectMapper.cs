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
            CreateMap<f_getproducts_By_NewArrivals, f_getproducts_By_NewArrivalsModel>().ReverseMap();

        }
    }
}
