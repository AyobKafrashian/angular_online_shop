using AngularEshop.DataLayer.Entities.Account;
using AngularEshop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AngularEshop.DataLayer.Entities.Orders
{
    public class Order : BaseEntity
    {
        #region Properties

        public long UserId { get; set; }

        public bool IsPay { get; set; }

        public DateTime? PaymentDay { get; set; }

        #endregion

        #region Relations
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        #endregion
    }
}
