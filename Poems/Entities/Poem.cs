using Common.Conventions;

namespace Poems.Entities
{
    public class Poem : IHasKey<long>
    {
        public long Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public double Distance { get; set; }

        public long PoetId { get; set; }
    }
}
