using Application.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class Mapping: Profile
{
    public Mapping()
    {
        
        CreateMap<ProdutoDTO, Produto>().ReverseMap();
    }
}
