namespace Shoppite.Infrastructure.Repository
{
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Shoppite.Core.Entities;
    using Shoppite.Core.Repositories;
    using Shoppite.Infrastructure.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ProductDetailRepository" />.
    /// </summary>
    public class ProductDetailRepository : IProductDetailRepsitory
    {
        /// <summary>
        /// Defines the _dbContext.
        /// </summary>
        protected readonly Shoppite_masterContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetailRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="Shoppite_masterContext"/>.</param>
        public ProductDetailRepository(Shoppite_masterContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// The productBasic.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductBasic}"/>.</returns>
        public async Task<ProductBasic> productBasic(Guid id)
        {
            var productdetails = await _dbContext.ProductBasic.Where(x => x.ProductGuid == id).FirstOrDefaultAsync();
            return productdetails;
        }

        /// <summary>
        /// The ProductImages.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{List{ProductImages}}"/>.</returns>
        public async Task<List<ProductImages>> ProductImages(Guid id)
        {
            var productImages = await _dbContext.ProductImages.Where(x => x.ProductGuid == id).ToListAsync();
            return productImages;
        }

        /// <summary>
        /// The productPrices.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductPrice}"/>.</returns>
        public async Task<ProductPrice> productPrices(Guid id)
        {
            var productprice = await _dbContext.ProductPrice.Where(x => x.ProductGuid == id).FirstOrDefaultAsync();
            return productprice;
        }

        /// <summary>
        /// The Get_Brand_Name.
        /// </summary>
        /// <param name="guid">The guid<see cref="Guid"/>.</param>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{Brands}"/>.</returns>
        public async Task<Brands> Get_Brand_Name(Guid guid, int id)
        {
            var GetBrandName = (from PB in _dbContext.ProductBrands
                                join B in _dbContext.Brands on PB.BrandId equals B.BrandId
                                where PB.ProductGuid == guid
                                select new Brands
                                {
                                    BrandName = B.BrandName,
                                    OrgId = B.OrgId,
                                }).FirstOrDefault();
            return GetBrandName;
        }

        /// <summary>
        /// Defines the catId.
        /// </summary>
        public int catId;

        /// <summary>
        /// The Get_Product_Cat.
        /// </summary>
        /// <param name="guid">The guid<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductCategory}"/>.</returns>
        public async Task<ProductCategory> Get_Product_Cat(Guid guid)
        {
            var GetCat = await _dbContext.ProductCategory.Where(x => x.ProductGuid == guid).FirstOrDefaultAsync();
            catId = (int)GetCat.CategoryId;
            return GetCat;
        }

        /// <summary>
        /// The F_Getproducts_By_CategoryID.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{List{f_getproducts_By_CategoryID}}"/>.</returns>
        public async Task<List<f_getproducts_By_CategoryID>> F_Getproducts_By_CategoryID(int id)
        {
            id = catId;

            string sql = "select * from f_getproducts_By_CategoryID(@ID)";

            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@ID", Value = id }
            };

            return await _dbContext.Set<f_getproducts_By_CategoryID>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
        }

        /// <summary>
        /// The AddIp.
        /// </summary>
        /// <param name="productRecentlyViewed">The productRecentlyViewed<see cref="ProductRecentlyViewed"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task AddIp(ProductRecentlyViewed productRecentlyViewed)
        {

            var UpdateIp = _dbContext.ProductRecentlyViewed.FirstOrDefault(x => x.ProductId == productRecentlyViewed.ProductId);
            if (UpdateIp == null)
            {
                _dbContext.ProductRecentlyViewed.Add(productRecentlyViewed);
            }
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// The GetIP.
        /// </summary>
        /// <param name="orgid">The orgid<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ProductRecentlyViewed}"/>.</returns>
        public async Task<ProductRecentlyViewed> GetIP(int orgid)
        {
            return await _dbContext.ProductRecentlyViewed.Where(x => x.OrgId == orgid).FirstOrDefaultAsync();
        }

        public async Task<List<f_getproducts_Recentlyviewed>> F_Getproducts_Recentlyviewed(string id , int orgid)
        {
            string sql = "select * from f_getproducts_Recentlyviewed(@IP,@orgid)";

            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@IP", Value = id },
                new SqlParameter { ParameterName = "@orgid", Value = orgid }
            };

            return await _dbContext.Set<f_getproducts_Recentlyviewed>().FromSqlRaw(sql, parms.ToArray()).ToListAsync();
        }
    }
}
