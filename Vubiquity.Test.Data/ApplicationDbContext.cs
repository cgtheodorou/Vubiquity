using Microsoft.EntityFrameworkCore;
using System;

namespace Vubiquity.Test.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Basket> Basket { get; set; }
        public DbSet<Models.BasketItem> BasketItem { get; set; }
        public DbSet<Models.Product> Product { get; set; }
    }
}
