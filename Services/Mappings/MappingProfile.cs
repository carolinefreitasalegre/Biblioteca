using AutoMapper;
using Domain.DTO.Response;
using Models.Models;

namespace Services.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Usuario, UsuarioResponse>();
        CreateMap<Livro, LivroResponse>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AnoPublicacao,
                opt => opt.MapFrom(src => src.AnoPublicacao.HasValue
                    ? src.AnoPublicacao.Value.ToDateTime(TimeOnly.MinValue)
                    : (DateTime?)null));
    }
}