using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiShopBB202.Models;

namespace MultiShopBB202.Context
{
    public class MultiDbContext : IdentityDbContext<AppUser>
    {
        public MultiDbContext(DbContextOptions<MultiDbContext> options) : base(options)
        {

        }

        public DbSet<Setting> Settings {get;set;}
        public DbSet<Category> Categories {get;set;}
        public DbSet<Product> Products {get;set;}
        public DbSet<Discount> Discounts {get;set;}
        public DbSet<Size> Sizes {get;set;}
        public DbSet<ProductSize> ProductSizes {get;set;}
        public DbSet<ProductColor> ProductColors {get;set;}
        public DbSet<Color> Colors {get;set;}
        public DbSet<ProductImage> ProductImages {get;set;}
    }
}
