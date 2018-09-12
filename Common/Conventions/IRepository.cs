using System.Linq;
using System.Threading.Tasks;

namespace Common.Conventions
{
    public interface IRepository
    {
        IQueryable<T> Query<T>() where T : class;
        Task<T> GetAsync<T>(object id) where T : class;
        Task<T> AddAsync<T>(T entity) where T : class;
        Task SaveAsync();
    }
}
