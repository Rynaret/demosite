namespace Common.Conventions.Queries
{
    public interface IQueryFactory
    {
        IQuery<TCriterion, TResult> Create<TCriterion, TResult>()
            where TCriterion : ICriterion;

        IQuery<TCriterion, TEntity, TResult> Create<TCriterion, TEntity, TResult>()
            where TCriterion : ICriterion;
    }
}