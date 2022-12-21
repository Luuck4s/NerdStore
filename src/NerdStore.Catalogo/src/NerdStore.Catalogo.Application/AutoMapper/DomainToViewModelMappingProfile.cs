using AutoMapper;
using NerdStore.Catalogo.Application.Dtos;
using NerdStore.Catalogo.Domain.Entities;

namespace NerdStore.Catalogo.Application.AutoMapper;

public class DomainToViewModelMappingProfile: Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<Categoria, CategoriaDto>();
        CreateMap<Produto, ProdutoDto>()
            .ForMember(d => d.Altura, o => o.MapFrom(s => s.Dimensoes.Altura))
            .ForMember(d => d.Largura, o => o.MapFrom(s => s.Dimensoes.Largura))
            .ForMember(d => d.Profundidade, o => o.MapFrom(s => s.Dimensoes.Profundidade));
    }
}