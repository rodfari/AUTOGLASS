using System.Linq.Expressions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace DbSqlLite.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    struct Situacao
    {
        public static string ATIVO => "ATIVO";
        public static string INATIVO => "INATIVO";
    }
    public ProdutoDbContext dbContext { get; set; }
    public ProdutoRepository(ProdutoDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<bool> CreateOrUpdateAsync(Produto produto)
    {
            if (produto.CodigoProduto == 0)
            {
                produto.Situacao = Situacao.ATIVO;
                produto.Validate();
                dbContext.Add(produto);
                await dbContext.SaveChangesAsync();
                return true;
            }
            produto.Validate();
            dbContext.Entry(produto).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return true;
    }
    public async Task<bool> DeleteAsync(int CodigoProduto)
    {
        try
        {
            var prod = await dbContext.Produtos.FirstAsync(x => x.CodigoProduto.Equals(CodigoProduto));
            prod.Situacao = Situacao.INATIVO;
            await dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public async Task<List<Produto>> GetAllAsync() => await dbContext.Produtos.ToListAsync();

    public async Task<Produto> GetByIdAsync(int CodigoProduto)
    {
        var prod = await dbContext
            .Produtos
            .FirstOrDefaultAsync(x => x.CodigoProduto.Equals(CodigoProduto))
            ?? throw new RegisterNotFoundException("Nenhum registro encontrado");
        return prod;
    }
}