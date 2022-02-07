﻿// <auto-generated />
using System;
using AngularEshop.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AngularEshop.DataLayer.Migrations
{
    [DbContext(typeof(AngularEshopDbContext))]
    partial class AngularEshopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Access.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("RoleTitle")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Access.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<long>("RoleId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Account.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("EmailActiveCode")
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsActivated");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Orders.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsPay");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<DateTime?>("PaymentDay");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Orders.OrderDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<long>("OrderId");

                    b.Property<int>("Price");

                    b.Property<long>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Product.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsExist");

                    b.Property<bool>("IsSpecial");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<int>("Price")
                        .HasMaxLength(100);

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Product.ProductCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<long?>("ParentId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("UrlTitle")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("ProductCategorys");
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Product.ProductComment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<long>("ProductId");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("ProductComments");
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Product.ProductGallery", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<long>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductGallerys");
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Product.ProductSelectedCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<long>("ProductCategoryId");

                    b.Property<long>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductSelectedCategorys");
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Product.ProductVisit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<long>("ProductId");

                    b.Property<string>("UserIp")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVisits");
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Site.Slider", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<string>("Link")
                        .HasMaxLength(500);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Slider");
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Access.UserRole", b =>
                {
                    b.HasOne("AngularEshop.DataLayer.Entities.Access.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AngularEshop.DataLayer.Entities.Account.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Orders.Order", b =>
                {
                    b.HasOne("AngularEshop.DataLayer.Entities.Account.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Orders.OrderDetail", b =>
                {
                    b.HasOne("AngularEshop.DataLayer.Entities.Orders.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AngularEshop.DataLayer.Entities.Product.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Product.ProductCategory", b =>
                {
                    b.HasOne("AngularEshop.DataLayer.Entities.Product.ProductCategory", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Product.ProductComment", b =>
                {
                    b.HasOne("AngularEshop.DataLayer.Entities.Product.Product", "Product")
                        .WithMany("ProductComments")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AngularEshop.DataLayer.Entities.Account.User", "User")
                        .WithMany("ProductComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Product.ProductGallery", b =>
                {
                    b.HasOne("AngularEshop.DataLayer.Entities.Product.Product", "Product")
                        .WithMany("ProductGalleryes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Product.ProductSelectedCategory", b =>
                {
                    b.HasOne("AngularEshop.DataLayer.Entities.Product.ProductCategory", "ProductCategory")
                        .WithMany("ProductSelectedCategorys")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AngularEshop.DataLayer.Entities.Product.Product", "Product")
                        .WithMany("ProductSelectedCategorys")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AngularEshop.DataLayer.Entities.Product.ProductVisit", b =>
                {
                    b.HasOne("AngularEshop.DataLayer.Entities.Product.Product", "Product")
                        .WithMany("ProductVisits")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
