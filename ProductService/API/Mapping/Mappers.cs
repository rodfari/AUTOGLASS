using API.ViewModels.ProdutoVM;
using Application.DTO;
using AutoMapper;

namespace API.Mapping;

public class Mappers: Profile
{
    public Mappers()
    {
        CreateMap<ProdutoRequestVM, ProdutoDTO>()
        .ReverseMap();
        CreateMap<ProdutoResponseVM, ProdutoDTO>()
        .ReverseMap();
    }
}