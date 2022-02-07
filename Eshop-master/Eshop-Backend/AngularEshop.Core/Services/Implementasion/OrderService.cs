using AngularEshop.Core.DTOs.Orders;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using AngularEshop.DataLayer.Entities.Orders;
using AngularEshop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Implementasion
{
    public class OrderService : IOrderService
    {
        #region Constructor
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly IUserServices _userService;
        private readonly IProductServices _productService;

        public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<OrderDetail> orderDetailRepository, IUserServices userService, IProductServices productService)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _userService = userService;
            _productService = productService;
        }
        #endregion

        #region Order
        public async Task<Order> CreateUserOrder(long userId)
        {
            var order = new Order()
            {
                UserId = userId
            };

            await _orderRepository.AddEntity(order);
            await _orderRepository.SaveChanges();

            return order;
        }

        public async Task<Order> GetUserOpenOrder(long userId)
        {
            var order = await _orderRepository.GetEntitiesQuery().Include(s => s.OrderDetails).ThenInclude(c => c.Product).SingleOrDefaultAsync(s => s.UserId == userId && !s.IsPay && !s.IsDelete);

            if (order == null)
                order = await CreateUserOrder(userId);

            return order;
        }

        #endregion

        #region OrderDetail

        public async Task<List<OrderDetail>> GetOrderDetails(long orderId)
        {
            return await _orderDetailRepository.GetEntitiesQuery().Where(c => c.OrderId == orderId).ToListAsync();
        }

        public async Task AddProdcutToOrder(long userId, long productId, int count)
        {
            var user = await _userService.GetUserById(userId);
            var product = await _productService.GetProductForUserOrder(productId);

            if (user != null && product != null)
            {
                var order = await GetUserOpenOrder(userId);

                if (count < 1) count = 1;


                var datails = await GetOrderDetails(order.Id);

                var ExistDatail = datails.SingleOrDefault(s => s.ProductId == productId && !s.IsDelete);

                if (ExistDatail != null)
                {
                    ExistDatail.Count += count;
                    _orderDetailRepository.UpdateEntity(ExistDatail);
                }
                else
                {
                    var orderDetail = new OrderDetail()
                    {
                        OrderId = order.Id,
                        ProductId = productId,
                        Count = count,
                        Price = product.Price
                    };
                    await _orderDetailRepository.AddEntity(orderDetail);
                }

                await _orderDetailRepository.SaveChanges();
            }
        }

        public async Task<List<OrderBasketDetailDTO>> GetUserBasketDetail(long userId)
        {
            var openOrder = await GetUserOpenOrder(userId);

            if (openOrder == null) return null;

            return openOrder.OrderDetails.Where(f=>!f.IsDelete).Select(f => new OrderBasketDetailDTO()
            {
                Id = f.Id,
                Count = f.Count,
                Price = f.Price,
                Title = f.Product.ProductName,
                ImageName = PathTools.Domain + PathTools.ProductImagePath + f.Product.ImageName,
                DateTime = f.CreateDate.ToString()
            }).OrderByDescending(c => c.DateTime).ToList();
        }

        public async Task RemoveOrderDetail(OrderDetail detail)
        {
            _orderDetailRepository.RemoveEntity(detail);
            await _orderDetailRepository.SaveChanges();
        }

        #endregion

        #region Dispose
        public void Dispose()
        {
            _orderRepository.Dispose();
            _orderDetailRepository.Dispose();
        }
        #endregion
    }
}
