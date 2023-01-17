using Shoppite.Application.Interfaces;
using Shoppite.Application.Models;
using Shoppite.Core.Interfaces;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
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
        public Task<MainModel> GetBrands(int orgid)
        {
            throw new NotImplementedException();
        }
    }
}
