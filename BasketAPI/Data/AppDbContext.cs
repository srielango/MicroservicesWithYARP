using Microsoft.EntityFrameworkCore;

namespace BasketAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCart>()
                .HasKey(sc => sc.UserName);
            modelBuilder.Entity<ShoppingCartItem>()
                .HasKey(sci => new {sci.UserName, sci.ProductName });
        }
    }
}
