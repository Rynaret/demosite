using Common.Conventions.Commands;

namespace Poems.Models.Contexts
{
    public class EstimatePoemContext : ICommandContext
    {
        public EstimatePoemContext(long poemId)
        {
            PoemId = poemId;
        }

        public long PoemId { get; }
    }
}
