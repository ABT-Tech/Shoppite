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

            var Get_Cat_List = await _BrandRepository.Sp_Getcat(orgid);
            main.GetSp_Getcat_ResultModel = ObjectMapper.Mapper.Map<List<sp_getcat_ResultModel>>(Get_Cat_List);

            var brands = await _BrandRepository.GetBrands(orgid);
             main.BrandsModel = ObjectMapper.Mapper.Map<List<BrandsModel>>(brands);

            //var GetCategoryByOrg = await _BrandRepository.GetCategoryBy_Org(orgid);
            //main.f_Getproducts_By_OrgIDModel = ObjectMapper.Mapper.Map<List<f_getproducts_By_OrgIDModel>>(GetCategoryByOrg);

            var getnewProduct = await _BrandRepository._Getproducts_By_NewArrivals(orgid);
            main.ProductNewArrivalModel = ObjectMapper.Mapper.Map<List<f_getproducts_By_NewArrivalsModel>>(getnewProduct);

            return main;
        }

        public async Task<MainModel> GetCategoryBy_Org(int orgid)
        {
            MainModel main = new MainModel();
            var GetNewProducts = await _BrandRepository.GetCategoryBy_Org(orgid);
            main.ProductNewArrivalModel = ObjectMapper.Mapper.Map<List<f_getproducts_By_NewArrivalsModel>>(GetNewProducts);
            return main;
        }

        public async Task<MainModel> Get_Product_By_Cat(int ID)
        {
            MainModel main = new MainModel();
            var Product_By_Cat = await _BrandRepository.Get_Product_By_Cat(ID);
            main.F_Getproducts_By_CategoryIDModels = ObjectMapper.Mapper.Map<List<f_getproducts_By_CategoryIDModel>>(Product_By_Cat);
            return main;
        }

        public async Task<MainModel> Sp_Getcat(int orgid)
        {
            MainModel main = new MainModel();
            var Get_Cat_List = await _BrandRepository.Sp_Getcat(orgid);
            main.GetSp_Getcat_ResultModel = ObjectMapper.Mapper.Map<List<sp_getcat_ResultModel>>(Get_Cat_List);
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
