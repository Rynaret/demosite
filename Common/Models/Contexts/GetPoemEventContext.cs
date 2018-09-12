namespace Common.Models.Contexts
{
    public class GetPoemEventContext : IntegrationEvent
    {
        public GetPoemEventContext(long personId)
        {
            PersonId = personId;
        }

        public long PersonId { get; }
    }
}
