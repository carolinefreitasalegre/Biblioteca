using AutoMapper;
using Domain.DTO;
using Domain.DTO.Response;
using Models.Models;

namespace Services.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Usuario, UsuarioResponse>();
        CreateMap<Livro, LivroResponse>()
            .ForMember(dest => dest.AnoPublicacao,
                opt => opt.MapFrom(src => src.AnoPublicacao.HasValue
                    ? src.AnoPublicacao.Value.ToDateTime(TimeOnly.MinValue)
                    : (DateTime?)null));

        CreateMap<LivroRequest, Livro>()
            .ForMember(dest => dest.CapaUrl, opt => opt.Ignore());
    }
}