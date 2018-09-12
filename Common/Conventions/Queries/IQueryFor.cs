using System.Threading.Tasks;

namespace Common.Conventions.Queries
{
    public interface IQueryFor<T>
    {
        Task<T> WithAsync<TCriterion>(TCriterion criterion)
            where TCriterion : ICriterion;
    }

    public interface IQueryFor<T, TEntity> where TEntity : class
    {
        Task<T> WithAsync<TCriterion>(TCriterion criterion)
            where TCriterion : ICriterion;
    }
}