using AutoMapper;
using Common.Models;
using Poems.Entities;
using Poems.Models.ExternalJsonModels;

namespace Poems.Models.Mappings
{
    public class PoemProfile : Profile
    {
        public PoemProfile()
        {
            CreateMap<RandomPoemResultModel, Poem>()
                .ForMember(x => x.Distance, y => y.Ignore())
                .ForMember(x => x.PoetId, y => y.Ignore())
                .ForMember(x => x.Id, y => y.Ignore());
            CreateMap<Poem, PoemViewModel>();
        }
    }
}
