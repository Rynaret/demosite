using AutoMapper;
using Common.Models;
using Common.Models.Reports;

namespace Reports.Models.Mappings
{
    public class PeopleReportProfile : Profile
    {
        public PeopleReportProfile()
        {
            CreateMap<PeopleViewModel, PeopleReport>()
                .ForMember(x => x.PoemContent, y => y.MapFrom(m => m.Poem == null ? "" : m.Poem.Content))
                .ForMember(x => x.PoemDistance, y => y.MapFrom(m => m.Poem == null ? 0 : m.Poem.Distance));
        }
    }
}
