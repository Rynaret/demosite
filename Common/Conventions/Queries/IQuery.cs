using System.Threading.Tasks;

namespace Common.Conventions.Queries
{
    public interface IQuery<in TCriterion, TResult>
        where TCriterion : ICriterion
    {
        Task<TResult> AskAsync(TCriterion criterion);
    }

    public interface IQuery<in TCriterion, TEntity, TResult>
        where TCriterion : ICriterion
    {
        Task<TResult> AskAsync(TCriterion criterion);
    }
}