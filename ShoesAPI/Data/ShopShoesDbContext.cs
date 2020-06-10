using Microsoft.EntityFrameworkCore;
using ShoesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesAPI.Data
{
    public class ShopShoesDbContext : DbContext
    {
        public ShopShoesDbContext(DbContextOptions<ShopShoesDbContext> options) : base(options)
        {

        }
        public DbSet<Shoes> Shoes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartLine> CartLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 1,
                CategoryName = "Giày Running",
                CategoryDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 2,
                CategoryName = "Giày Sneaker Thời Trang",
                CategoryDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 3,
                CategoryName = "Sandal",
                CategoryDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 4,
                CategoryName = "Giày da",
                CategoryDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book"
            });

            modelBuilder.Entity<Shoes>().HasData(new Shoes
            {
                ShoesId = 1,
                Name = "Duramo 9",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book",
                Price = 80,
                CategoryId = 1,
                ImageUrl = "/assets/images/shoes-item/duramo9_1.jpg",
                ImageThumbnailUrl = "/assets/images/shoes-item/duramo9_2.jpg",
                IsOnSale = false,
                IsOnNew = true,
                IsOnStock = true,
            });
            modelBuilder.Entity<Shoes>().HasData(new Shoes
            {
                ShoesId = 2,
                Name = "NMD XR1",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book",
                Price = 167,
                CategoryId = 2,
                ImageUrl = "/assets/images/shoes-item/nmd_1.jpg",
                ImageThumbnailUrl = "/assets/images/shoes-item/nmd_2.jpg",
                IsOnSale = true,
                IsOnNew = false,
                IsOnStock = true,
            });
            modelBuilder.Entity<Shoes>().HasData(new Shoes
            {
                ShoesId = 3,
                Name = "Sandal Fitsal",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book",
                Price = 40,
                CategoryId = 3,
                ImageUrl = "/assets/images/shoes-item/sandal_1.jpg",
                ImageThumbnailUrl = "/assets/images/shoes-item/sandal_2.jpg",
                IsOnSale = true,
                IsOnNew = true,
                IsOnStock = true,
            });
            modelBuilder.Entity<Shoes>().HasData(new Shoes
            {
                ShoesId = 4,
                Name = "Giày Da Thượng Đình",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book",
                Price = 15,
                CategoryId = 4,
                ImageUrl = "/assets/images/shoes-item/da_1.jpg",
                ImageThumbnailUrl = "/assets/images/shoes-item/da_2.jpg",
                IsOnSale = false,
                IsOnNew = true,
                IsOnStock = true,
            });
        }
    }
}
