namespace Common.Conventions.Queries
{
    public interface IQueryResolver
    {
        IQuery<TCriterion, TResult> Resolve<TCriterion, TResult>() where TCriterion : ICriterion;
        IQuery<TCriterion, TEntity, TResult> Resolve<TCriterion, TEntity, TResult>() where TCriterion : ICriterion;
    }
}