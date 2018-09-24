namespace Common.Models.Contexts
{
    public class CreatePoemForPersonEventContext : IntegrationEvent
    {
        public CreatePoemForPersonEventContext(long personId)
        {
            PersonId = personId;
        }

        public long PersonId { get; }
    }
}
