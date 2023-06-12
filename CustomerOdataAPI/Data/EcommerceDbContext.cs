using CustomerOdataAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace CustomerOdataAPI.Data
{
    public class EcommerceDbContext: DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Order { get; set; }
    }
}
