using Common.Conventions;

namespace Poems.Options
{
    public class ExternalApiOptions : IConfigurableOptions
    {
        public string RandomPoemUrl { get; set; }
    }
}
