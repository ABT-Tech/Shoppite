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

        public async Task<MainModel> GetBrands(int orgId)
        {
            return await _BrandService.GetBrands(orgId);
        }

        public async Task<MainModel> GetNewProduct(int orgId)
        {
            return await _BrandService.GetNewProducts(orgId);
        }

        public async Task<MainModel> GetnewProduct(int orgid)
        {
            return await _BrandService._Getproducts_By_NewArrivals(orgid);
        }
    }
}
