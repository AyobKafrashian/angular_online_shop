using AngularEshop.Core.DTOs.Products;
using AngularEshop.DataLayer.Entities.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Interfaces
{
    public interface IProductServices : IDisposable
    {
        #region Profuct
        Task AddProduct(Product product);

        Task UpdateProduct(Product product);

        Task<FilterProductsDTO> FilterProducts(FilterProductsDTO filter);

        Task<Product> GetProductById(long id);

        Task<List<Product>> GetProductRelatedProduct(long productId);

        Task<bool> IsExistProductById(long productId);

        Task<Product> GetProductForUserOrder(long productId);
        #endregion

        #region Product Category
        Task<List<ProductCategory>> GetAllActiveProductCategories();
        #endregion

        #region Product Gallery
        Task<List<ProductGallery>> GetProductActiveGalleries(long productId);
        #endregion

        #region Product Comments
        Task AddCommentToProduct(ProductComment comment);
        Task<ProductCommetDTO> AddProductComment(AddProductCommentDTO comment , long userId);
        Task<List<ProductCommetDTO>> GetActiveProductComments(long productId);
        #endregion
    }
}
