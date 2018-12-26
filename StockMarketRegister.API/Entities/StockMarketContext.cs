using Microsoft.EntityFrameworkCore;

namespace StockMarketRegister.API.Entities
{
    public class StockMarketContext : DbContext
    {
        public StockMarketContext(DbContextOptions<StockMarketContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
