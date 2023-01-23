using AutoMapper;
using Shoppite.Application.Interfaces;
using Shoppite.Application.Models;
using Shoppite.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoppite.Web.Services
{
    public class CategoryPageService : ICategoryPageService
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryPageService(ICategoryService categoryService,IMapper mapper)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<CategoryMasterModel>> GetCategoryList(int orgId)
        {
            var list = await _categoryService.GetCategoryList(orgId);
            var mapped = _mapper.Map<IEnumerable<CategoryMasterModel>>(list);
            return mapped;
        }
        public async Task<CategoryMasterModel> GetBannerImage(int orgId)
        {
            var image = await _categoryService.GetBannerImage(orgId);
            var mapped = _mapper.Map<CategoryMasterModel>(image);
            return mapped;

        }
        public async Task<CategoryMasterModel> GetBottomImage(int orgId)
        {
            var image = await _categoryService.GetBottomImage(orgId);
            var mapped = _mapper.Map<CategoryMasterModel>(image);
            return mapped;
        }
        public async Task<List<CategoryMasterModel>> GetProductList(int orgId)
        {
            var productList = await _categoryService.GetProductList(orgId);
            var mapped = _mapper.Map<List<CategoryMasterModel>>(productList);
            return mapped;
        }
    }
}
