using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using DbSqlLite.EntityConfiguration;

namespace DbSqlLite;

public class ProdutoDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }

    public ProdutoDbContext(DbContextOptions options): base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
    }
}