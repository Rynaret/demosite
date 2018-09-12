using Common.Conventions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Implementations
{
    public class EfRepository : IRepository
    {
        private readonly DbContext dbContext;

        public EfRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            return (await GetDbSet<T>().AddAsync(entity)).Entity;
        }

        public Task<T> GetAsync<T>(object id) where T : class
        {
            return GetDbSet<T>().FindAsync(id);
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return GetDbSet<T>();
        }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        private DbSet<T> GetDbSet<T>() where T : class
        {
            return dbContext.Set<T>();
        }
    }
}
