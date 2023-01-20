using AutoMapper.Internal.Mappers;
using Shoppite.Application.Interfaces;
using Shoppite.Application.Mapper;
using Shoppite.Application.Models;
using Shoppite.Core.Interfaces;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Services
{
    public class BrandServices : IBrandServices
    {
        private readonly IBrandRepository _BrandRepository;
        private readonly IAppLogger<BrandServices> _logger;
        public BrandServices(IBrandRepository brandRepository, IAppLogger<BrandServices> appLogger)
        {
            _BrandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _logger = appLogger ?? throw new ArgumentNullException(nameof(appLogger));
        }
        public async Task<MainModel> GetBrands(int orgid)
        {
            MainModel main = new MainModel();
            var brands = await _BrandRepository.GetBrands(orgid);
             main.BrandsModel = ObjectMapper.Mapper.Map<List<BrandsModel>>(brands);

            //var GetNewProducts = await _BrandRepository.GetNewProducts(orgid);
            //main.ProductNewArrivalModel = ObjectMapper.Mapper.Map<List<f_getproducts_By_NewArrivalsModel>>(GetNewProducts);

            var getnewProduct = await _BrandRepository._Getproducts_By_NewArrivals(orgid);
            main.ProductNewArrivalModel = ObjectMapper.Mapper.Map<List<f_getproducts_By_NewArrivalsModel>>(getnewProduct);

            return main;
        }

        public async Task<MainModel> GetNewProducts(int orgid)
        {
            MainModel main = new MainModel();
            var GetNewProducts = await _BrandRepository.GetNewProducts(orgid);
            main.ProductNewArrivalModel = ObjectMapper.Mapper.Map<List<f_getproducts_By_NewArrivalsModel>>(GetNewProducts);
            return main;
        }

        public async Task<MainModel> _Getproducts_By_NewArrivals(int orgid)
        {
            MainModel main = new MainModel();
            var getnewProduct = await _BrandRepository._Getproducts_By_NewArrivals(orgid);
            main.ProductNewArrivalModel = ObjectMapper.Mapper.Map<List<f_getproducts_By_NewArrivalsModel>>(getnewProduct);
            return main;
        }
    }
}
