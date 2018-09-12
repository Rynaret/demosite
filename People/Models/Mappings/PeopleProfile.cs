using AutoMapper;
using Common.Models;
using People.Models.ExternalJsonModels;

namespace People.Models.Mappings
{
    public class PeopleProfile : Profile
    {
        public PeopleProfile()
        {
            CreateMap<RandomUserModel, Entities.People>()
                .ForMember(x => x.PictureMedium, y => y.MapFrom(m => m.Picture.Medium))
                .ForMember(x => x.Street, y => y.MapFrom(m => m.Location.Street))
                .ForMember(x => x.City, y => y.MapFrom(m => m.Location.City))
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.Quote, y => y.Ignore())
                .ForMember(x => x.FirstName, y => y.MapFrom(m => m.Name.First))
                .ForMember(x => x.LastName, y => y.MapFrom(m => m.Name.Last));

            CreateMap<Entities.People, PeopleViewModel>()
                .ForMember(x => x.Poem, y => y.Ignore());
        }
    }
}
