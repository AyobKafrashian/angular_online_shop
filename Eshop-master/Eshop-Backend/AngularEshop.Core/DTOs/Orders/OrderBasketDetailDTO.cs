using System;
using System.Collections.Generic;
using System.Text;

namespace AngularEshop.Core.DTOs.Orders
{
    public class OrderBasketDetailDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string ImageName { get; set; }
        public int Count { get; set; }
        public string DateTime { get; set; }
    }
}
