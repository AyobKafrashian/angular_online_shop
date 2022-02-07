using AngularEshop.Core.DTOs.Paging;
using AngularEshop.Core.DTOs.Products;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using AngularEshop.Core.Utilities.Extensions.Paging;
using AngularEshop.DataLayer.Entities.Product;
using AngularEshop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static AngularEshop.Core.DTOs.Products.FilterProductsDTO;

namespace AngularEshop.Core.Services.Implementasion
{
    public class ProductServices : IProductServices
    {
        #region Constructor
        private IGenericRepository<Product> _productRepository;
        private IGenericRepository<ProductCategory> _productCategoryRepository;
        private IGenericRepository<ProductGallery> _productGalleryRepository;
        private IGenericRepository<ProductSelectedCategory> _productSelectedCategoryRepository;
        private IGenericRepository<ProductVisit> _productVisitRepository;
        private IGenericRepository<ProductComment> _productCommentRepository;

        public ProductServices(IGenericRepository<Product> productRepository, IGenericRepository<ProductCategory> productCategoryRepository, IGenericRepository<ProductGallery> productGalleryRepository, IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository, IGenericRepository<ProductVisit> productVisitRepository, IGenericRepository<ProductComment> productCommentRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productGalleryRepository = productGalleryRepository;
            _productSelectedCategoryRepository = productSelectedCategoryRepository;
            _productVisitRepository = productVisitRepository;
            _productCommentRepository = productCommentRepository;
        }
        #endregion

        #region Product
        public async Task AddProduct(Product product)
        {
            await _productRepository.AddEntity(product);
            await _productRepository.SaveChanges();
        }

        public async Task<bool> IsExistProductById(long productId)
        {
            return await _productRepository.GetEntitiesQuery().AnyAsync(s => s.Id == productId);
        }

        public async Task<Product> GetProductById(long id)
        {
            return await _productRepository.GetEntitByID(id);
        }

        public async Task UpdateProduct(Product product)
        {
            _productRepository.UpdateEntity(product);
            await _productRepository.SaveChanges();
        }

        public async Task<Product> GetProductForUserOrder(long productId)
        {
            return await _productRepository.GetEntitiesQuery().SingleOrDefaultAsync(u => u.Id == productId && !u.IsDelete);
        }

        public async Task<FilterProductsDTO> FilterProducts(FilterProductsDTO filter)
        {
            //IQueryRable<Product> = _context.product;
            var productQuery = _productRepository.GetEntitiesQuery().AsQueryable();

            //FilterPrice
            switch (filter.OrderBy)
            {
                case ProductOrderBy.PriceAsc:
                    productQuery = productQuery.OrderBy(s => s.Price);
                    break;

                case ProductOrderBy.PriceDec:
                    productQuery = productQuery.OrderByDescending(s => s.Price);
                    break;

            }


            //Search Title Filter
            if (!string.IsNullOrEmpty(filter.Title))
            {
                productQuery = productQuery.Where(s => s.ProductName.Contains(filter.Title));
            }

            //Price Filter
            productQuery = productQuery.Where(s => s.Price >= filter.StartPrice);

            if (filter.EndPrice != 0)
            {
                productQuery = productQuery.Where(c => c.Price <= filter.EndPrice);
            }

            if (filter.categories != null && filter.categories.Any())
            {
                productQuery = productQuery.SelectMany(s => s.ProductSelectedCategorys.Where(f => filter.categories.Contains(f.ProductCategoryId)).Select(t => t.Product));
            }

            //اعداد محصولات
            var count = (int)Math.Ceiling(productQuery.Count() / (double)filter.TakeEntity);


            var pager = Pager.Build(count, filter.pageId, filter.TakeEntity);


            var products = await productQuery.Paging(pager).ToListAsync();

            //برای این ما خود فیلتر را برمیگردونیم که اگر در فیلتر محصولات استرینگی پر باشد پر باقی بماند
            return filter.SetProducts(products).SetPaging(pager);
        }

        public async Task<List<Product>> GetProductRelatedProduct(long productId)
        {
            var product = await _productRepository.GetEntitByID(productId);

            if (product == null) return null;

            var productCategoriesList = await _productSelectedCategoryRepository.GetEntitiesQuery().Where(s => s.ProductId == productId).Select(f => f.ProductCategoryId).ToListAsync();

            var relatedProduct = await _productRepository.GetEntitiesQuery().SelectMany(s => s.ProductSelectedCategorys.Where(f => productCategoriesList.Contains(f.ProductCategoryId)).Select(t => t.Product)).Where(s => s.Id != productId).OrderByDescending(c => c.CreateDate).Take(4).ToListAsync();

            return relatedProduct;
        }
        #endregion

        #region Product Categories

        public async Task<List<ProductCategory>> GetAllActiveProductCategories()
        {
            return await _productCategoryRepository.GetEntitiesQuery().Where(c => !c.IsDelete).ToListAsync();
        }
        #endregion

        #region Product Gallery
        public async Task<List<ProductGallery>> GetProductActiveGalleries(long productId)
        {
            return await _productGalleryRepository.GetEntitiesQuery().Where(s => s.ProductId == productId && !s.IsDelete).Select(s => new ProductGallery
            {
                ProductId = s.ProductId,
                Id = s.Id,
                ImageName = s.ImageName,
                CreateDate = s.CreateDate
            }).ToListAsync();
        }
        #endregion

        #region Product Comments
        public async Task AddCommentToProduct(ProductComment comment)
        {
            await _productCommentRepository.AddEntity(comment);
            await _productCommentRepository.SaveChanges();
        }

        public async Task<List<ProductCommetDTO>> GetActiveProductComments(long productId)
        {
            return await _productCommentRepository.GetEntitiesQuery().Include(c => c.User).Where(c => c.ProductId == productId && !c.IsDelete).OrderByDescending(c => c.CreateDate).Select(s => new ProductCommetDTO
            {
                Id = s.Id,
                Text = s.Text,
                UserId = s.UserId,
                UserFullName = s.User.FirstName + " " + s.User.LastName,
                CreateDate = s.CreateDate.ToShamsi()
            }).ToListAsync();
        }

        public async Task<ProductCommetDTO> AddProductComment(AddProductCommentDTO comment, long userId)
        {
            var commentData = new ProductComment
            {
                ProductId = comment.ProductId,
                Text = comment.Text,
                UserId = userId
            };

            await _productCommentRepository.AddEntity(commentData);
            await _productCommentRepository.SaveChanges();

            return new ProductCommetDTO
            {
                Id = commentData.Id,
                CreateDate = commentData.CreateDate.ToShamsi(),
                Text = commentData.Text,
                UserId = userId,
                UserFullName = ""
            };
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _productCategoryRepository?.Dispose();
            _productGalleryRepository?.Dispose();
            _productRepository?.Dispose();
            _productSelectedCategoryRepository?.Dispose();
            _productVisitRepository?.Dispose();
            _productCommentRepository?.Dispose();
        }
        #endregion
    }
}
