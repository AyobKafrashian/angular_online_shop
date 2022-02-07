using AngularEshop.DataLayer.Entities.Access;
using AngularEshop.DataLayer.Entities.Account;
using AngularEshop.DataLayer.Entities.Orders;
using AngularEshop.DataLayer.Entities.Product;
using AngularEshop.DataLayer.Entities.Site;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngularEshop.DataLayer.Context
{
    public class AngularEshopDbContext : DbContext
    {
        #region Constructor
        public AngularEshopDbContext(DbContextOptions<AngularEshopDbContext> options) : base(options)
        {

        }
        #endregion

        #region DbSet
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Slider> Slider { get; set; }
        #endregion

        #region Product
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategorys { get; set; }
        public DbSet<ProductSelectedCategory> ProductSelectedCategorys { get; set; }
        public DbSet<ProductGallery> ProductGallerys { get; set; }
        public DbSet<ProductVisit> ProductVisits { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        #endregion

        #region Order
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        #endregion

        //برای حذف فیزیکی به کار میره
        #region disable cascading delete in datebase
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var casecades = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach(var item in casecades)
            {
                item.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
