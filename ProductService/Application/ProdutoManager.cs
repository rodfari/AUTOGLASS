using System.Linq.Expressions;
using Application.DTO;
using Application.Ports;
using Application.Requests;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports;

namespace Application;

public class ProdutoManager : IProdutoManager
{
    private readonly IProdutoRepository repository;
    private IMapper mapper { get; set; }

    public ProdutoManager(IProdutoRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
    public async Task<ProdutoResponse> CreateAsync(ProdutoRequest request)
    {
        try
        {

            Produto produto = mapper.Map<Produto>(request.ProdutoDTO);
            await repository.CreateOrUpdateAsync(produto);
            return new ProdutoResponse
            {
                Success = true,
            };
        }
        catch (ExpireDateException ex)
        {
            return new ProdutoResponse
            {
                Success = false,
                ErrorCode = Domain.Enums.ErrorsCode.DATE_EXCEPTION,
                Message = ex.Message
            };
        }catch (NotNullOrEmptyStringException ex)
        {
            return new ProdutoResponse
            {
                Success = false,
                ErrorCode = Domain.Enums.ErrorsCode.EMPTY_STRING,
                Message = ex.Message
            };
        }

    }
    public async Task<ProdutoResponse> DeleteAsync(int codigoProduto)
    {
        bool saved = await repository.DeleteAsync(codigoProduto);

        return new ProdutoResponse
        {
            Success = saved,
            Message = saved == true ? "" : "Não foi possível excluir o produto."
        };
    }
    
    public async Task<ProdutoResponse> GetAllAsync()
    {
        var produtos = await repository.GetAllAsync();
        var produtosDTO = mapper.Map<List<ProdutoDTO>>(produtos);
        return new ProdutoResponse
        {
            Success = true,
            ListProdutoDTO = produtosDTO
        };
    }

    // public async Task<List<ProdutoResponse>> FiltrarProduto(Expression<Func<Produto, bool>> expression){
    //     repository.FiltrarProdutos()
    //         .Where(expression)
    // }
    public async Task<ProdutoResponse> GetAllAsync(Expression<Func<Produto, bool>> expression, int currentPage, int amount)
    {
        var produtos = await repository.GetAllAsync(expression, currentPage, amount);
        var produtosDTO = mapper.Map<List<ProdutoDTO>>(produtos);
        return new ProdutoResponse
        {
            Success = true,
            ListProdutoDTO = produtosDTO
        };
    }
    public async Task<ProdutoResponse> GetByIdAsync(int Id)
    {
        try
        {
            var result = await repository.GetByIdAsync(Id);
            var dto = mapper.Map<ProdutoDTO>(result);
            return new ProdutoResponse
            {
                Success = true,
                ProdutoDTO = dto
            };

        }
        catch (RegisterNotFoundException ex)
        {
            return new ProdutoResponse
            {
                Success = false,
                ErrorCode = Domain.Enums.ErrorsCode.NOT_FOUND,
                Message = ex.Message
            };
        }

    }
}