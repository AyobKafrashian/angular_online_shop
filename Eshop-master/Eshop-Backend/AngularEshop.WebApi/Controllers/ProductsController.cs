using AngularEshop.Core.DTOs.Products;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using AngularEshop.Core.Utilities.Extensions.Identity;
using AngularEshop.DataLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularEshop.WebApi.Controllers
{
    public class ProductsController : SiteBaseController
    {
        #region Constructor
        private IProductServices _productService;

        public ProductsController(IProductServices productServices)
        {
            _productService = productServices;
        }
        #endregion

        #region Products 
        [HttpGet("filter-products")]
        public async Task<IActionResult> GetProducts([FromQuery] FilterProductsDTO filter)
        {
            var products = await _productService.FilterProducts(filter);

            return JsonResponseStatus.Success(products);
        }
        #endregion

        #region Get Product Categories
        [HttpGet("product-active-categories")]
        public async Task<IActionResult> GetProductCategories()
        {
            return JsonResponseStatus.Success(await _productService.GetAllActiveProductCategories());
        }
        #endregion

        #region Get Single Product
        [HttpGet("single-product/{id}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            var product = await _productService.GetProductById(id);

            #region Product Galleries
            var productGalleries = await _productService.GetProductActiveGalleries(id);
            #endregion

            #region Product Related
            var relatedProduct = await _productService.GetProductRelatedProduct(id);
            #endregion

            if (product != null)
            {
                return JsonResponseStatus.Success(new { product = product, galleries = productGalleries, relatedProduct = relatedProduct });
            }
            return JsonResponseStatus.NotFound();
        }
        #endregion

        #region Product Comment

        #region Get All Activated Comment
        [HttpGet("product-comments/{id}")]
        public async Task<IActionResult> GetProductComment(long id)
        {
            var comment = await _productService.GetActiveProductComments(id);

            return JsonResponseStatus.Success(comment);
        }
        #endregion

        #region Add Comment Product
        [HttpPost("add-product-comment")]
        public async Task<IActionResult> AddProductComment([FromBody] AddProductCommentDTO comment)
        {
            if (!User.Identity.IsAuthenticated) return JsonResponseStatus.Error(new { message = "لطفا ابتدا وارد سایت شوید" });

            if (!await _productService.IsExistProductById(comment.ProductId)) return JsonResponseStatus.NotFound();

            var userId = User.GetUserId();

            var res = await _productService.AddProductComment(comment, userId);

            return JsonResponseStatus.Success(res);
        }
        #endregion

        #endregion
    }
}
