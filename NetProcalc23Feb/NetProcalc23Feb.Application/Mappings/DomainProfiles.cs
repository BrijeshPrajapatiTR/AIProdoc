using AutoMapper;
using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Application.Mappings;

public class DomainProfiles : Profile
{
    public DomainProfiles()
    {
        CreateMap<Party, Party>();
        CreateMap<Case, Case>();
        CreateMap<Obligation, Obligation>();
        CreateMap<Payment, Payment>();
    }
}
