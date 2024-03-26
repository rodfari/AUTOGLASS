using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Ports;

public interface IProdutoRepository
{
    Task<Produto> GetByIdAsync(int CodigoProduto);
    Task<List<Produto>> GetAllAsync();
    Task<bool> CreateOrUpdateAsync(Produto produto);
    Task<bool> DeleteAsync(int CodigoProduto);

}