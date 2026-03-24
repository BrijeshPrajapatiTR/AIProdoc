using AutoMapper;
using NetProcalc23Feb.Domain.Entities;
using NetProcalc23Feb.Web.Models;

namespace NetProcalc23Feb.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Party, PartyDto>().ReverseMap();
        CreateMap<Case, CaseDto>().ReverseMap();
        CreateMap<Obligation, ObligationDto>().ReverseMap();
        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<Judgment, JudgmentDto>().ReverseMap();
    }
}
