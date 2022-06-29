using AutoMapper;
using Rise.PhoneDirectory.Store.Dtos;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Service.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Person, PersonDto>().ForMember(nq => nq.Id, sq => sq.MapFrom(x => x.PersonId)).ReverseMap();
            CreateMap<ContactInformation, ContactInformationDto>().ForMember(nq => nq.Id, sq => sq.MapFrom(x => x.ContactInformationId)).ReverseMap();
            CreateMap<Report, ReportDto>().ForMember(nq => nq.Id, sq => sq.MapFrom(x => x.ReportId)).ReverseMap();
            CreateMap<Person, PersonWithContactInfoDto>().ForMember(nq => nq.Id, sq => sq.MapFrom(x => x.PersonId)).ForPath(dest => dest.ContactInformation, opt => opt.MapFrom(src => src.ContactInformations));
        }
    }
}
