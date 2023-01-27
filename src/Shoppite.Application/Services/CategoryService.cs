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
        public async Task<IEnumerable<CategoryProductModel>> GetProductList(int orgId)
        {
            List<CategoryProductModel> categoryProductsModel = new List<CategoryProductModel>();
            var productList = await _categoryRepository.GetProductList(orgId);
            var Products = productList.GroupBy(x => new { x.maincatid,x.maincaturlpath,x.Category_Id,x.category_name}, (key, g) => new { CatId = key.Category_Id,maincatid=key.maincatid, CatName = key.category_name,maincatname=key.maincaturlpath , Products = g.ToList() });
            foreach (var Product in Products) {
                CategoryProductModel categoryProductModel = new CategoryProductModel();
                categoryProductModel.CategoryId = Product.CatId;
                categoryProductModel.maincatid = Product.maincatid;
                categoryProductModel.CategoryName = Product.CatName;
                categoryProductModel.maincaturlpath = Product.maincatname;
                categoryProductModel.ProductsDetails = categoryProductModel.ProductsDetails == null ? new List<ProductBaseModel>() : categoryProductModel.ProductsDetails;
                foreach (var prod in Product.Products) 
                {
                    ProductBaseModel productBaseModel = new ProductBaseModel();
                    productBaseModel.ProductGuid = prod.ProductGUID;
                    productBaseModel.ProductId = prod.ProductId;
                    productBaseModel.ProductName = prod.ProductName;
                    productBaseModel.CoverImage = prod.image;
                    categoryProductModel.ProductsDetails.Add(productBaseModel);
                }
                categoryProductsModel.Add(categoryProductModel);
            }
            return categoryProductsModel;
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
    }
}
