using Microsoft.EntityFrameworkCore;
 
namespace eCommerce.Models
{
    public class CommerceContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public CommerceContext(DbContextOptions<CommerceContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}