using AutoMapper;
using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Application.Profiles;

public class ClarionProfile : Profile
{
    public ClarionProfile()
    {
        CreateMap<ProcedureDef, ProcedureDef>();
        CreateMap<MenuItem, MenuItem>();
    }
}
