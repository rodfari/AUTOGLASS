using System.Linq.Expressions;
using Application.Requests;
using Application.Responses;
using Domain.Entities;

namespace Application.Ports;

public interface IProdutoManager
{
    Task<ProdutoResponse> GetByIdAsync(int Id);
    Task<ProdutoResponse> GetAllAsync();
    Task<ProdutoResponse> CreateAsync(ProdutoRequest request);
    Task<ProdutoResponse> DeleteAsync(int codigoProduto);
}