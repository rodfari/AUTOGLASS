using System.Linq.Expressions;
using Application.DTO;
using Application.Ports;
using Application.Requests;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Ports;

namespace Application;

public class ProdutoManager : IProdutoManager
{
    private readonly IProdutoRepository repository;
    private IMapper mapper {get; set;}

    public ProdutoManager(IProdutoRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
    public async Task<ProdutoResponse> CreateAsync(ProdutoRequest request)
    {
        Produto produto = mapper.Map<Produto>(request.ProdutoDTO);
        produto.Validate();
        await repository.CreateOrUpdateAsync(produto);
        return new ProdutoResponse{
            Success = true,
        };
    }
    public async Task<ProdutoResponse> DeleteAsync(int codigoProduto)
    {
        bool saved = await repository.DeleteAsync(codigoProduto) > 0;

        return new ProdutoResponse{
            Success = saved
        };
    }
    public async Task<ProdutoResponse> GetAllAsync()
    {
        var produtos =  await repository.GetAllAsync();
        var produtosDTO = mapper.Map<List<ProdutoDTO>>(produtos);
        return new ProdutoResponse{
             Success = true,
             ListProdutoDTO = produtosDTO
        };
    }
    public async Task<ProdutoResponse> GetByIdAsync(int Id)
    {
        var result = await repository.GetByIdAsync(Id);
        var dto = mapper.Map<ProdutoDTO>(result);
        return new ProdutoResponse{
            Success = true,
            ProdutoDTO = dto
        };

    }
}