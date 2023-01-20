using AutoMapper;
using AutoMapper.Internal.Mappers;
using Shoppite.Application.Interfaces;
using Shoppite.Application.Mapper;
using Shoppite.Application.Models;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task <IEnumerable<CategoryMasterModel>> GetCategoryList(int orgId)
        {
            var categoryList = await _categoryRepository.GetCategoryList(orgId);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CategoryMasterModel>>(categoryList);
            return mapped;
        }
        public async Task<CategoryMasterModel>  GetBannerImage(int orgId)
        {
           var image= await _categoryRepository.GetBannerImage(orgId);
            var mapped = ObjectMapper.Mapper.Map<CategoryMasterModel>(image);
            return mapped;           
        }
        public async Task<CategoryMasterModel> GetBottomImage(int orgId)
        {
            var image = await _categoryRepository.GetBottomImage(orgId);
            var mapped = ObjectMapper.Mapper.Map<CategoryMasterModel>(image);
            return mapped;
        }
        public async Task<IEnumerable<CategoryMasterModel>> GetProductList(int orgId)
        {
            var productList = await _categoryRepository.GetProductList(orgId);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CategoryMasterModel>>(productList);
            return mapped;
        }
    }
}
