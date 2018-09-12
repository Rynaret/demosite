using Common.Conventions;

namespace People.Options
{
    public class ExternalApiOptions : IConfigurableOptions
    {
        public string RandomUserUrl { get; set; }
        public string QuoteUrl { get; set; }
    }
}
