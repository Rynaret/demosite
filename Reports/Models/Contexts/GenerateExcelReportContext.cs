using Common.Conventions.Commands;
using Common.Models;
using System.Collections;
using System.Collections.Generic;

namespace Reports.Models.Contexts
{
    public class GenerateExcelReportContext : ICommandContext
    {
        public GenerateExcelReportContext(IEnumerable data, List<ColumnInfo> metadata)
        {
            Data = data;
            Metadata = metadata;
        }

        public IEnumerable Data { get; }
        public List<ColumnInfo> Metadata { get; }

        public string GeneratedFilePath { get; set; }
    }
}
