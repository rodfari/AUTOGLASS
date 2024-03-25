using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Ports;

public interface IProdutoRepository
{
    Task<Produto> GetByIdAsync(int CodigoProduto);
    Task<List<Produto>> GetAllAsync();
    Task<int> CreateOrUpdateAsync(Produto produto);
    Task<int> DeleteAsync(int CodigoProduto);

}