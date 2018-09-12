using Common.Conventions;

namespace Poems.Models.Contexts
{
    public class CreatePoemContext : ICreateEntityContext
    {
        public CreatePoemContext(long peopleId)
        {
            PeopleId = peopleId;
        }

        public long PeopleId { get; }
        public long IdAfterCreate { get; set; }
    }
}
