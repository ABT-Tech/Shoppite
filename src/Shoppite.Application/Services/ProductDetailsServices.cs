namespace Shoppite.Application.Services
{
    using Microsoft.AspNetCore.Http;
    using Shoppite.Application.Interfaces;
    using Shoppite.Application.Mapper;
    using Shoppite.Application.Models;
    using Shoppite.Core.Entities;
    using Shoppite.Core.Interfaces;
    using Shoppite.Core.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ProductDetilServices" />.
    /// </summary>
    public class ProductDetilServices : IProductDetailServices
    {
        /// <summary>
        /// Defines the _ProductDetailRepsitory.
        /// </summary>
        private readonly IProductDetailRepsitory _ProductDetailRepsitory;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly IAppLogger<ProductDetilServices> _logger;

        /// <summary>
        /// Defines the _accessor.
        /// </summary>
        private IHttpContextAccessor _accessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetilServices"/> class.
        /// </summary>
        /// <param name="ProductDetailRepsitory">The ProductDetailRepsitory<see cref="IProductDetailRepsitory"/>.</param>
        /// <param name="appLogger">The appLogger<see cref="IAppLogger{ProductDetilServices}"/>.</param>
        /// <param name="accessor">The accessor<see cref="IHttpContextAccessor"/>.</param>
        public ProductDetilServices(IProductDetailRepsitory ProductDetailRepsitory, IAppLogger<ProductDetilServices> appLogger, IHttpContextAccessor accessor)
        {
            _ProductDetailRepsitory = ProductDetailRepsitory ?? throw new ArgumentNullException(nameof(ProductDetailRepsitory));
            _logger = appLogger ?? throw new ArgumentNullException(nameof(appLogger));
            _accessor = accessor;
        }

        /// <summary>
        /// The GetProductDetails.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductDetailModel}"/>.</returns>
        public async Task<ProductDetailModel> GetProductDetails(Guid id, int orgid)
        {
            int x = 1;
            ProductDetailModel productDetailModel = new ProductDetailModel();
            var productDetail = await _ProductDetailRepsitory.productBasic(id);
            productDetailModel.ProductBasicModel = ObjectMapper.Mapper.Map<ProductBasicModel>(productDetail);

            var ProductImages = await _ProductDetailRepsitory.ProductImages(id);
            productDetailModel.ProductImagesModel = ObjectMapper.Mapper.Map<List<ProductImagesModel>>(ProductImages);

            var ProductPrice = await _ProductDetailRepsitory.productPrices(id);
            productDetailModel.ProductPriceModel = ObjectMapper.Mapper.Map<ProductPriceModel>(ProductPrice);

            var Brand_Name = await _ProductDetailRepsitory.Get_Brand_Name(id, x);
            productDetailModel.BrandsModel = ObjectMapper.Mapper.Map<BrandsModel>(Brand_Name);

            var ProductCatId = await _ProductDetailRepsitory.Get_Product_Cat(id);
            productDetailModel.ProductCategoryModel = ObjectMapper.Mapper.Map<ProductCategoryModel>(ProductCatId);

            var Product_By_Cat = await _ProductDetailRepsitory.F_Getproducts_By_CategoryID(x);
            productDetailModel.f_Getproducts_By_CategoryIDModels = ObjectMapper.Mapper.Map<List<f_getproducts_By_CategoryIDModel>>(Product_By_Cat);

            string ipAddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            ProductRecentlyViewed productRecently = new ProductRecentlyViewed();
            productRecently.Ip = ipAddress;
            productRecently.ProductId = productDetailModel.ProductBasicModel.ProductId;
            productRecently.Insertdate = DateTime.Now;
            productRecently.OrgId = productDetailModel.ProductBasicModel.OrgId;

            var productRecent = ObjectMapper.Mapper.Map<ProductRecentlyViewed>(productRecently);
            await _ProductDetailRepsitory.AddIp(productRecent);

            var ProductRecent = await _ProductDetailRepsitory.F_Getproducts_Recentlyviewed(ipAddress, orgid);
            productDetailModel.f_Getproducts_RecentlyviewedModel = ObjectMapper.Mapper.Map<List<f_getproducts_RecentlyviewedModel>>(ProductRecent);

            return productDetailModel;
        }

        /// <summary>
        /// The GetProductImages.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{List{ProductDetailModel}}"/>.</returns>
        public async Task<List<ProductDetailModel>> GetProductImages(Guid id)
        {
            ProductDetailModel productDetailModel = new ProductDetailModel();

            var ProductImages = await _ProductDetailRepsitory.ProductImages(id);
            productDetailModel.ProductImagesModel = ObjectMapper.Mapper.Map<List<ProductImagesModel>>(ProductImages);

            return null;
        }

        /// <summary>
        /// The GetProductPrice.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductDetailModel}"/>.</returns>
        public async Task<ProductDetailModel> GetProductPrice(Guid id)
        {
            ProductDetailModel productDetailModel = new ProductDetailModel();

            var ProductPrice = await _ProductDetailRepsitory.productPrices(id);
            productDetailModel.ProductPriceModel = ObjectMapper.Mapper.Map<ProductPriceModel>(ProductPrice);

            return productDetailModel;
        }

        /// <summary>
        /// The Get_Brand_Name.
        /// </summary>
        /// <param name="guid">The guid<see cref="Guid"/>.</param>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ProductDetailModel}"/>.</returns>
        public async Task<ProductDetailModel> Get_Brand_Name(Guid guid, int id)
        {
            ProductDetailModel productDetailModel = new ProductDetailModel();
            var Brand_Name = await _ProductDetailRepsitory.Get_Brand_Name(guid, id);
            productDetailModel.BrandsModel = ObjectMapper.Mapper.Map<BrandsModel>(Brand_Name);
            return productDetailModel;
        }

        /// <summary>
        /// The Get_Product_Cat.
        /// </summary>
        /// <param name="guid">The guid<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductDetailModel}"/>.</returns>
        public async Task<ProductDetailModel> Get_Product_Cat(Guid guid)
        {
            ProductDetailModel productDetailModel = new ProductDetailModel();
            var ProductCatId = await _ProductDetailRepsitory.Get_Product_Cat(guid);
            productDetailModel.ProductCategoryModel = ObjectMapper.Mapper.Map<ProductCategoryModel>(ProductCatId);
            return productDetailModel;
        }

        /// <summary>
        /// The Get_Similar_Product.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{List{ProductDetailModel}}"/>.</returns>
        public async Task<List<ProductDetailModel>> Get_Similar_Product(int id)
        {
            ProductDetailModel main = new ProductDetailModel();
            var Product_By_Cat = await _ProductDetailRepsitory.F_Getproducts_By_CategoryID(id);
            main.f_Getproducts_By_CategoryIDModels = ObjectMapper.Mapper.Map<List<f_getproducts_By_CategoryIDModel>>(Product_By_Cat);
            return null;
        }

        /// <summary>
        /// The AddIp.
        /// </summary>
        /// <param name="detailModel">The detailModel<see cref="ProductDetailModel"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task AddIp(ProductDetailModel detailModel)
        {
            var productRecent = ObjectMapper.Mapper.Map<ProductRecentlyViewed>(detailModel.ProductRecentlyViewedModel);
            await _ProductDetailRepsitory.AddIp(productRecent);
        }

        /// <summary>
        /// The GetIP.
        /// </summary>
        /// <param name="orgid">The orgid<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ProductDetailModel}"/>.</returns>
        public async Task<ProductDetailModel> GetIP(int orgid)
        {
            ProductDetailModel main = new ProductDetailModel();
            var GetNewProducts = await _ProductDetailRepsitory.GetIP(orgid);
            main.ProductRecentlyViewedModel = ObjectMapper.Mapper.Map<ProductRecentlyViewedModel>(GetNewProducts);
            return main;
        }

        public async Task<List<ProductDetailModel>> Get_Recently_Product(string id, int orgid)
        {
            ProductDetailModel productDetailModel = new ProductDetailModel();
            var ProductCatId = await _ProductDetailRepsitory.F_Getproducts_Recentlyviewed(id,orgid);
            productDetailModel.f_Getproducts_RecentlyviewedModel = ObjectMapper.Mapper.Map<List<f_getproducts_RecentlyviewedModel>>(ProductCatId);
            return null;
        }
    }
}
