namespace Common.Conventions
{
    public interface IHasKey<TKey>
    {
        TKey Id { get; set; }
    }
}
