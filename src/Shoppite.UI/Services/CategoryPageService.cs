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
        public async Task<List<CategoryMasterModel>> GetTopBannerImage(int orgId)
        {
            var image = await _categoryService.GetTopBannerImage(orgId);
            var mapped = _mapper.Map<List<CategoryMasterModel>>(image);
            return mapped;
        }
        public async Task<List<CategoryMasterModel>> GetMiddelBannerImage(int orgId)
        {
            var image = await _categoryService.GetMiddelBannerImage(orgId);
            var mapped = _mapper.Map<List<CategoryMasterModel>>(image);
            return mapped;
        }
        public async Task<IEnumerable<MainCategoryModel>> GetProductList(int orgId)
        {
            var productList = await _categoryService.GetProductList(orgId);
            return productList;
        }
        public async Task<List<CategoryMasterModel>> GetCategories(int CategoryId)
        {
            var categories= await _categoryService.GetCategories(CategoryId);
            return categories;
        }
        public async Task<CategoryMasterModel> DisplayLogo(int orgId)
        {
            var logo = await _categoryService.DisplayLogo(orgId);
            return logo;
        }
        public async Task<List<CategoryMasterModel>> GetHorizontalBanner(int orgId)
        {
            var image = await _categoryService.GetHorizontalBanner(orgId);
            var mapped = _mapper.Map<List<CategoryMasterModel>>(image);
            return mapped;
        }
        public async Task<List<CategoryMasterModel>> GetAllProductByCategory(int CategoryId)
        {
            var productList = await _categoryService.GetAllProductByCategory(CategoryId);
            return productList;
        }
        public async Task<List<CategoryMasterModel>> GetAllSubCategories(int MainCategoryId)
        {
            var subactegories = await _categoryService.GetAllSubCategories(MainCategoryId);
            return subactegories;
        }
        public async Task<List<AttributeSetupModel>> GetAllAttributes(int orgId)
        {
            var attribute = await _categoryService.GetAllAttributes(orgId);
            return attribute;
       }
    }
}
