using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using AngularEshop.Core.Utilities.Extensions.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularEshop.WebApi.Controllers
{
    public class OrderController : SiteBaseController
    {
        #region Cosntructor
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        #endregion

        #region Add Product To Order 
        [HttpGet("add-order")]
        public async Task<IActionResult> AddProductToOrder(long productId, int count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.GetUserId();
                await _orderService.AddProdcutToOrder(userId, productId, count);
                return JsonResponseStatus.Success(new { message = "محصول با موفقیت به سبد خرید شما افزوده شد", returnData = await _orderService.GetUserBasketDetail(userId) });
            }
            return JsonResponseStatus.Error(new { message = "برای افزودن محصول به سبد خرید ابتدا لاگین کنید" });
        }
        #endregion

        #region Get User Basket Detail
        [HttpGet("get-order-details")]
        public async Task<IActionResult> GetUserBasketDetail()
        {
            if (User.Identity.IsAuthenticated)
            {
                var details = await _orderService.GetUserBasketDetail(User.GetUserId());
                return JsonResponseStatus.Success(details);
            }
            return JsonResponseStatus.Error();

        }
        #endregion

        #region Remove Order detail from basket
        [HttpGet("remove-order-details/{detailid}")]
        public async Task<IActionResult> RemoveOrderDetail(int detailid)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userOpenOrder = await _orderService.GetUserOpenOrder(User.GetUserId());

                var detail = userOpenOrder.OrderDetails.SingleOrDefault(s => s.Id == detailid);

                if (detail != null)
                {
                    await _orderService.RemoveOrderDetail(detail);
                    return JsonResponseStatus.Success(await _orderService.GetUserBasketDetail(User.GetUserId()));
                }
            }

            return JsonResponseStatus.Error();
        }
        #endregion

    }
}
