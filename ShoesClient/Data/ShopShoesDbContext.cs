using Microsoft.EntityFrameworkCore;
using ShoesClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesClient.Data
{
    public class ShopShoesDbContext:DbContext
    {
        public ShopShoesDbContext(DbContextOptions<ShopShoesDbContext> options) : base(options)
        {

        }
        public DbSet<Shoes> Shoes { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
