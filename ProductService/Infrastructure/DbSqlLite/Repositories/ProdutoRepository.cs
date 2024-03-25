using System.Linq.Expressions;
using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace DbSqlLite.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    struct Situacao{
        public static string ATIVO => "ATIVO";
        public static string INATIVO => "INATIVO";
    }
    public ProdutoDbContext dbContext { get; set; }
    public ProdutoRepository(ProdutoDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<int> CreateOrUpdateAsync(Produto produto)
    {
        if(produto.CodigoProduto == 0){
            dbContext.Add(produto);
            int result = await dbContext.SaveChangesAsync();
            return result;
        }

        dbContext.Entry(produto).State = EntityState.Modified;
        return await dbContext.SaveChangesAsync();
    }
    public async Task<int> DeleteAsync(int CodigoProduto)
    {
        var prod = await dbContext.Produtos.FirstAsync(x => x.CodigoProduto.Equals(CodigoProduto));
        prod.Situacao = Situacao.INATIVO;
        return await dbContext.SaveChangesAsync();
    }
    public async Task<List<Produto>> GetAllAsync() => await dbContext.Produtos.ToListAsync();

    public async Task<Produto> GetByIdAsync(int CodigoProduto)
    {
        var prod = await dbContext.Produtos.FirstAsync(x => x.CodigoProduto.Equals(CodigoProduto));
        return prod;
    }
}