using AngularEshop.Core.DTOs.Orders;
using AngularEshop.DataLayer.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Interfaces
{
    public interface IOrderService : IDisposable
    {
        #region Order
        Task<Order> CreateUserOrder(long userId);
        Task<Order> GetUserOpenOrder(long userId);
        #endregion

        #region OrderDetail
        Task AddProdcutToOrder(long userId, long productId, int count);
        Task<List<OrderDetail>> GetOrderDetails(long orderId);
        Task<List<OrderBasketDetailDTO>> GetUserBasketDetail(long userId);
        Task RemoveOrderDetail(OrderDetail detail); 
        #endregion
    }
}
