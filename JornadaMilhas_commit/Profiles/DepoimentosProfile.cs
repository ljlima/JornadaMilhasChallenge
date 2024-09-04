using AutoMapper;
using JornadaMilhas.Data.Dtos.DepoimentosDtos;
using JornadaMilhas.Models;

namespace JornadaMilhas.Profiles;

public class DepoimentosProfile : Profile
{
    public DepoimentosProfile()
    {
        CreateMap<CreateDepoimentoDto, Depoimentos>();
        CreateMap<UpdateDepoimentoDto, Depoimentos>();
        CreateMap<Depoimentos,ReadDepoimentoDto>();
    }
}
