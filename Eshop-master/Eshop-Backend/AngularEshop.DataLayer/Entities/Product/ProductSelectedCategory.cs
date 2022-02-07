using AngularEshop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AngularEshop.DataLayer.Entities.Product
{
   public class ProductSelectedCategory:BaseEntity
    {
        public long ProductId { get; set; }
        public long ProductCategoryId { get; set; }

        #region Relations
        public ProductCategory ProductCategory { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}
