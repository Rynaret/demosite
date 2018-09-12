namespace Common.Models.Contexts
{
    public class ReportIsReadyEventContext : IntegrationEvent
    {
        public ReportIsReadyEventContext(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; }
    }
}
