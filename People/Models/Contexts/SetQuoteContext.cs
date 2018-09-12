using Common.Conventions.Commands;

namespace People.Models.Contexts
{
    public class SetQuoteContext : ICommandContext
    {
        public SetQuoteContext(long peopleId)
        {
            PeopleId = peopleId;
        }

        public long PeopleId { get; }
    }
}
