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
        public async Task<List<CategoryMasterModel>> GetTopBannerImage(int orgId)
        {
           var image= await _categoryRepository.GetTopBannerImage(orgId);
            var mapped = ObjectMapper.Mapper.Map<List<CategoryMasterModel>>(image);
            return mapped;           
        }
        public async Task<List<CategoryMasterModel>> GetMiddelBannerImage(int orgId)
        {
            var image = await _categoryRepository.GetMiddelBannerImage(orgId);
            var mapped = ObjectMapper.Mapper.Map<List<CategoryMasterModel>>(image);
            return mapped;
        }
        public async Task<IEnumerable<MainCategoryModel>> GetProductList(int orgId)
        {
            List<MainCategoryModel> mainCategory = new List<MainCategoryModel>();
            var mainCatlist = await _categoryRepository.GetProductList(orgId);
            var categories = mainCatlist.GroupBy(x => new { x.maincatid, x.maincaturlpath, }, (key, g) => new { CatId = key.maincatid, Catname = key.maincaturlpath, categories = g.ToList() });
            foreach (var category in categories)
            {
                MainCategoryModel mainCategoryModel = new MainCategoryModel();
                mainCategoryModel.maincatid = category.CatId;
                mainCategoryModel.maincaturlpath = category.Catname;
                mainCategoryModel.SubcategoryDetails = mainCategoryModel.SubcategoryDetails == null ? new List<CategoryProductModel>() : mainCategoryModel.SubcategoryDetails;
                foreach (var Product in category.categories)
                {
                    CategoryProductModel categoryProductModel = new CategoryProductModel();
                    categoryProductModel.CategoryId = Product.Category_Id;
                    categoryProductModel.CategoryName = Product.category_name;
                    mainCategoryModel.SubcategoryDetails.Add(categoryProductModel);
                    categoryProductModel.ProductsDetails = categoryProductModel.ProductsDetails == null ? new List<ProductBaseModel>() : categoryProductModel.ProductsDetails;
                    foreach (var prod in category.categories.Where(a => a.ProductId.Equals(Product.ProductId)))
                    {
                        ProductBaseModel productBaseModel = new ProductBaseModel();
                        productBaseModel.ProductGuid = prod.ProductGUID;
                        productBaseModel.ProductId = prod.ProductId;
                        productBaseModel.ProductName = prod.ProductName;
                        productBaseModel.OldPrice = prod.OldPrice;
                        productBaseModel.Price = prod.Price;
                        productBaseModel.CoverImage = prod.image;
                        categoryProductModel.ProductsDetails.Add(productBaseModel);
                    }
                }
                mainCategory.Add(mainCategoryModel);
            }
            return mainCategory;
        }
        public async Task<List<CategoryMasterModel>> GetCategories(int CategoryId)
        {
            var categories = await _categoryRepository.GetCategories(CategoryId);
            var mapped = ObjectMapper.Mapper.Map<List<CategoryMasterModel>>(categories);
            return mapped;
        }
        public async Task<CategoryMasterModel> DisplayLogo(int orgId)
        {
            var logo = await _categoryRepository.DisplayLogo(orgId);
            var mapped = ObjectMapper.Mapper.Map< CategoryMasterModel > (logo);
            return mapped;
        }
        public async Task<List<CategoryMasterModel>> GetHorizontalBanner(int orgId)
        {
            var image = await _categoryRepository.GetHorizontalBanner(orgId);
            var mapped = ObjectMapper.Mapper.Map<List<CategoryMasterModel>>(image);
            return mapped;
        }
        public async Task<List<CategoryMasterModel>> GetAllProductByCategory(int orgId)
        {
            var products = await _categoryRepository.GetAllProductByCategory(orgId);
            var mapped = ObjectMapper.Mapper.Map<List<CategoryMasterModel>>(products);
            return mapped;
        }
        public async Task<List<CategoryMasterModel>> GetAllSubCategories(int orgId)
        {
            var categories = await _categoryRepository.GetAllSubCategories(orgId);
            var mapped = ObjectMapper.Mapper.Map<List<CategoryMasterModel>>(categories);
            return mapped;
        }
        public async Task<List<AttributeSetupModel>> GetAllAttributes(int orgID)
        {
            List<AttributeSetupModel> attribute = new List<AttributeSetupModel>();
            var attributes = await _categoryRepository.GetAllAttributes(orgID);
            var specifications = attributes.GroupBy(x => new { x.AttributeName,x.AttributeId}, (key, g) => new { specName=key.AttributeName,specId=key.AttributeId, specific = g.ToList() });
            foreach (var sp in specifications)
            {
                AttributeSetupModel specification = new AttributeSetupModel();
                specification.AttributeId = sp.specId;
                specification.AttributeName = sp.specName;
                specification.specifications = specification.specifications == null ? new List<SpecificationSetupModel>() : specification.specifications;
                foreach (var prod in sp.specific)
                {
                    SpecificationSetupModel attributespec = new SpecificationSetupModel();
                    attributespec.SpecificationId = prod.SpecificationId;
                    attributespec.SpecificationName = prod.SpecificationName;
                   specification.specifications.Add(attributespec);
                }
                attribute.Add(specification);
            }
            return attribute;
        }
        public async Task<List<CategoryMasterModel>> GetAllProductByAttribute(int CategoryId, string AttributeName)
        {
            var attributes = await _categoryRepository.GetAllProductByAttribute(CategoryId, AttributeName);
            var mapped = ObjectMapper.Mapper.Map<List<CategoryMasterModel>>(attributes);
            return mapped;
        }
    }
}
