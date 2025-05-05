using Microsoft.EntityFrameworkCore;

namespace Store.Data
{
    public class DbContextFactory
    {
        private readonly IConfiguration _configuration;

        public DbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataContext CreateDbContext()
        {
            return new DataContext(new DbContextOptionsBuilder<DataContext>()
                .UseNpgsql(_configuration.GetConnectionString("DefaultConnection"))
                .Options);
        }

    }
}
