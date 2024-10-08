﻿using Attract.Domain.Entities.Attract;
using AttractDomain.Configuration;
using AttractDomain.Entities.Attract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Attract.Infrastructure.Data
{
    public class AppDbContext: IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductQuantity> ProductQuantities { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<AvailableSize>  AvailableSizes { get; set; }
        public DbSet<Tag>  Tags { get; set; }
        public DbSet<ProductTag>  ProductTags { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails{ get; set; }
        public DbSet<Bill> Bills{ get; set; }
        public DbSet<SliderValue> SliderValue{ get; set; }
        //public DbSet<ProductAvailableSize> ProductAvailableSizes{ get; set; }
        //public DbSet<ProductColor> ProductColors{ get; set; }
        public DbSet<CustomSubCategory> customSubCategories{ get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Country> Countries{ get; set; }
        public DbSet<Slider> Sliders{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BillConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductQuantityConfiguration());
            modelBuilder.ApplyConfiguration(new SubCategoryConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductColorConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductAvailableSizeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTagConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
