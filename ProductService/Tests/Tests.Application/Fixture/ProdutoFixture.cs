using Application.DTO;
using Domain.Entities;
using Domain.Structures;

namespace Tests.Application.Fixture;

public class ProdutoFixture
{
    public static ProdutoDTO DtoInserirProduto => new ProdutoDTO
    {
        CnpjFornecedor = "93.134.039/0001-82",
        CodigoFornecedor = "KJK-DLSKA",
        DataFabricacao = DateTime.Now.AddMonths(-6),
        DataValidade = DateTime.Now,
        Descricao = "teste inserindo produto",
        Situacao = ProdutoSituacao.ATIVO
    };

    public static List<Produto> ListProduto => new List<Produto>()
    {
            new Produto
            {
                CnpjFornecedor = "93.134.039/0001-82",
                CodigoFornecedor = "KJK-DLSKA",
                DataFabricacao = DateTime.Now.AddMonths(-6),
                DataValidade = DateTime.Now,
                Descricao = "PRODUTO 1",
                Situacao = ProdutoSituacao.ATIVO
            },
            new Produto
            {
                CnpjFornecedor = "93.134.039/0001-82",
                CodigoFornecedor = "KJK-DLSKA",
                DataFabricacao = DateTime.Now.AddMonths(-6),
                DataValidade = DateTime.Now,
                Descricao = "PRODUTO 2",
                Situacao = ProdutoSituacao.ATIVO
            },
            new Produto
            {
                CnpjFornecedor = "93.134.039/0001-42",
                CodigoFornecedor = "KJK-DLSKA",
                DataFabricacao = DateTime.Now.AddMonths(-6),
                DataValidade = DateTime.Now,
                Descricao = "PRODUTO 3",
                Situacao = ProdutoSituacao.INATIVO
            },
            new Produto
            {
                CnpjFornecedor = "93.134.039/0001-82",
                CodigoFornecedor = "KJK-DLSKA",
                DataFabricacao = DateTime.Now.AddMonths(-6),
                DataValidade = DateTime.Now,
                Descricao = "PRODUTO 4",
                Situacao = ProdutoSituacao.ATIVO
            },
            new Produto
            {
                CnpjFornecedor = "93.134.039/0001-82",
                CodigoFornecedor = "KJK-DLSKA",
                DataFabricacao = DateTime.Now.AddMonths(-6),
                DataValidade = DateTime.Now,
                Descricao = "PRODUTO 5",
                Situacao = ProdutoSituacao.ATIVO
            },
            new Produto
            {
                CnpjFornecedor = "93.134.039/0001-82",
                CodigoFornecedor = "KJK-DLSKA",
                DataFabricacao = DateTime.Now.AddMonths(-6),
                DataValidade = DateTime.Now,
                Descricao = "PRODUTO 6",
                Situacao = ProdutoSituacao.ATIVO
            },
            new Produto
            {
                CnpjFornecedor = "93.134.039/0001-01",
                CodigoFornecedor = "KJK-DLSKA",
                DataFabricacao = DateTime.Now.AddMonths(-6),
                DataValidade = DateTime.Now,
                Descricao = "PRODUTO 7",
                Situacao = ProdutoSituacao.INATIVO
            },
            new Produto
            {
                CnpjFornecedor = "93.134.039/0001-82",
                CodigoFornecedor = "KJK-DLSKA",
                DataFabricacao = DateTime.Now.AddMonths(-6),
                DataValidade = DateTime.Now,
                Descricao = "PRODUTO 8",
                Situacao = ProdutoSituacao.ATIVO
            },
            new Produto
            {
                CnpjFornecedor = "93.134.039/0001-00",
                CodigoFornecedor = "KJK-DLSKA",
                DataFabricacao = DateTime.Now.AddMonths(-6),
                DataValidade = DateTime.Now,
                Descricao = "PRODUTO 9",
                Situacao = ProdutoSituacao.INATIVO
            },
            new Produto
            {
                CnpjFornecedor = "93.134.039/0001-82",
                CodigoFornecedor = "KJK-DLSKA",
                DataFabricacao = DateTime.Now.AddMonths(-6),
                DataValidade = DateTime.Now,
                Descricao = "PRODUTO 10",
                Situacao = ProdutoSituacao.ATIVO
            }
    };

}