namespace Common.Conventions.Queries
{
    public interface IQueryForResolver
    {
        IQueryFor<T> Resolve<T>();
        IQueryFor<T, TEntity> ResolveGeneric<T, TEntity>() where TEntity : class;
    }
}