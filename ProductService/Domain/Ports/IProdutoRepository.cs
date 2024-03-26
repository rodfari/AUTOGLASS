using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Ports;

public interface IProdutoRepository
{
    Task<Produto> GetByIdAsync(int CodigoProduto);
    IQueryable<Produto> FiltrarProdutos();
    Task<List<Produto>> GetAllAsync();
    Task<List<Produto>> GetAllAsync(
        Expression<Func<Produto, bool>> expression,
        int currentPage, 
        int amount);
    Task<bool> CreateOrUpdateAsync(Produto produto);
    Task<bool> DeleteAsync(int CodigoProduto);

}