using AutoMapper;
using JornadaMilhas.Data.Dtos.DestinosDtos;
using JornadaMilhas.Models;

namespace JornadaMilhas.Profiles;

public class DestinosProfile : Profile
{
    public DestinosProfile()
    {
        CreateMap<CreateDestinoDto,Destinos>();
        CreateMap<Destinos, ReadDestinosDto>();
        CreateMap<UpdateDestinosDto, Destinos>();
    }
}
