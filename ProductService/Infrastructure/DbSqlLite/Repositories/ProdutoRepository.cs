using System.Linq.Expressions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports;
using Domain.Structures;
using Microsoft.EntityFrameworkCore;

namespace DbSqlLite.Repositories;

public class ProdutoRepository : IProdutoRepository
{

    public ProdutoDbContext dbContext { get; set; }
    public ProdutoRepository(ProdutoDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<bool> CreateOrUpdateAsync(Produto produto)
    {
        if (produto.CodigoProduto == 0)
        {
            produto.Situacao = ProdutoSituacao.ATIVO;
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
            prod.Situacao = ProdutoSituacao.INATIVO;
            await dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public async Task<List<Produto>> GetAllAsync() => await dbContext.Produtos.ToListAsync();

    public IQueryable<Produto> FiltrarProdutos() => dbContext.Produtos.AsQueryable();

    public async Task<List<Produto>> GetAllAsync(
        Expression<Func<Produto, bool>> expression,
        int currentPage, int amount)
    {
        var query = dbContext.Produtos.Where(expression)
        .Skip(currentPage * amount).Take(amount);
        return await query.ToListAsync();
    }

    public async Task<Produto> GetByIdAsync(int CodigoProduto)
    {
        var prod = await dbContext
            .Produtos
            .FirstOrDefaultAsync(x => x.CodigoProduto.Equals(CodigoProduto))
            ?? throw new RegisterNotFoundException("Nenhum registro encontrado");
        return prod;
    }
}