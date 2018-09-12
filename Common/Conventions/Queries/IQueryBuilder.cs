namespace Common.Conventions.Queries
{
    public interface IQueryBuilder
    {
        IQueryFor<TResult> For<TResult>();

        IQueryFor<TResult, TEntity> For<TEntity, TResult>() where TEntity : class;
    }
}