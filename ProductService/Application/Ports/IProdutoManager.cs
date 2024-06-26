using System.Linq.Expressions;
using Application.Requests;
using Application.Responses;
using Domain.Entities;

namespace Application.Ports;

public interface IProdutoManager
{
    Task<ProdutoResponse> GetByIdAsync(int Id);
    Task<ProdutoResponse> GetAllAsync(Expression<Func<Produto, bool>> expression, int currentPage, int amount);
    Task<ProdutoResponse> CreateAsync(ProdutoRequest request);
    Task<ProdutoResponse> UpdateAsync(ProdutoRequest request);
    Task<ProdutoResponse> DeleteAsync(int codigoProduto);
}