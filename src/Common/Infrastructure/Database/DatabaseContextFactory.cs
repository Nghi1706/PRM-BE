using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Common.Infrastructure.Database
{
    public interface IDatabaseContextFactory<T> where T : BaseDbContext
    {
        T CreateDbContext();
        T CreateDbContext(string connectionString);
    }

    public class DatabaseContextFactory<T> : IDatabaseContextFactory<T> where T : BaseDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly DbContextOptions<T> _options;

        public DatabaseContextFactory(IConfiguration configuration, DbContextOptions<T> options)
        {
            _configuration = configuration;
            _options = options;
        }

        public T CreateDbContext()
        {
            return (T)Activator.CreateInstance(typeof(T), _options)!;
        }

        public T CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            optionsBuilder.UseSqlServer(connectionString);
            return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options)!;
        }
    }
}
