using AutoMapper;
using Shoppite.Application.Interfaces;
using Shoppite.Application.Models;
using Shoppite.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Services
{
    public class BrandPageServices:IBrandPageServices
    {
        private readonly IBrandServices _BrandService;
        private readonly IMapper _mapper;

        public BrandPageServices(IBrandServices BrandService, IMapper mapper)
        {
            _BrandService = BrandService ?? throw new ArgumentNullException(nameof(BrandService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<MainModel> CategoryMater(int orgid)
        {
            return await _BrandService.CategoryMaster(orgid);
        }

        public async Task<MainModel> GetBrands(int orgId)
        {
            return await _BrandService.GetBrands(orgId);
        }

        public async Task<MainModel> GetCategoryBy_Org(int orgId)
        {
            return await _BrandService.GetCategoryBy_Org(orgId);
        }

        public async Task<MainModel> GetnewProduct(int orgid)
        {
            return await _BrandService._Getproducts_By_NewArrivals(orgid);
        }

        public async Task<MainModel> Get_Product_By_Cat(int ID)
        {
            return await _BrandService.Get_Product_By_Cat(ID);
        }

        public async Task<MainModel> Sp_Getcat(int orgid)
        {
            return await _BrandService.Sp_Getcat(orgid);
        }
        public async Task<List<F_getproducts_By_BrandIdModel>> GetProductsByBrand(int orgId, int BrandId)
        {
            var categories = await _BrandService.GetProductsByBrand(orgId, BrandId);
            var mapped = _mapper.Map<List<F_getproducts_By_BrandIdModel>>(categories);
            return mapped;
        }
    }
}
