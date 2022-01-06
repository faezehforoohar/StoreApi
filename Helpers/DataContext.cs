using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoreApi.Entities;

namespace StoreApi.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<PriceListD> PriceListDs { get; set; }

    }
}