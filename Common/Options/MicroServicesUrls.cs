using Common.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Options
{
    public class MicroServicesUrls : IConfigurableOptions
    {
        public string PeopleService { get; set; }
        public string PoemService { get; set; }
        public string ReportService { get; set; }
    }
}
