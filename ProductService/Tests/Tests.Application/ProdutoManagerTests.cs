using Application;
using Application.DTO;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using Domain.Ports;
using Moq;

namespace Tests.Application;

public class ProdutoManagerTests
{
    [Fact]
    public async void CreateAsyncDeveRetornarSuccess()
    {
        var produto = new Produto();

        var ProdutoRequest = new ProdutoRequest
        {
            ProdutoDTO = new ProdutoDTO
            {
                CnpjFornecedor = "93.134.039/0001-82",
                CodigoFornecedor = "KJK-DLSKA",
                DataFabricacao = DateTime.Now.AddMonths(-6),
                DataValidade = DateTime.Now,
                Descricao = "teste inserindo produto",
                Situacao = "Ativo"
            }
        };

        var fakeRepo = new Mock<IProdutoRepository>();
        fakeRepo.Setup(x => x.CreateOrUpdateAsync(produto))
           .Returns(Task.FromResult(true));
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ProdutoDTO, Produto>().ReverseMap());
        var map = config.CreateMapper();
        // When

        ProdutoManager manager = new ProdutoManager(fakeRepo.Object, map);
        var result = await manager.CreateAsync(ProdutoRequest);

        Assert.True(result.Success);
    }

    [Theory]
    [InlineData(12)]
    [InlineData(13)]
    [InlineData(155)]
    public async void GetByIdAsyncDeveRetornarProdutoComCodigoEquivalente(int id)
    {
        // Given
        var fakeRepo = new Mock<IProdutoRepository>();
        fakeRepo.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(new Produto
        {
            CnpjFornecedor = "93.134.039/0001-82",
            CodigoFornecedor = "KJK-DLSKA",
            CodigoProduto = id,
            DataFabricacao = DateTime.Now,
            DataValidade = DateTime.Now,
            Descricao = "fuhsdiuhfds",
            Situacao = "Ativo"
        }
       ));
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ProdutoDTO, Produto>().ReverseMap());
        var map = config.CreateMapper();
        // When

        ProdutoManager manager = new ProdutoManager(fakeRepo.Object, map);
        var result = await manager.GetByIdAsync(id);

        // Then
        Assert.True(result.Success);
        Assert.Equal(id, result.ProdutoDTO.CodigoProduto);
    }


    [Fact]
    public async void GetAllAsyncDeveRetornarArrayMaiorQueZero()
    {
        // Given
        var fakeRepo = new Mock<IProdutoRepository>();
        var list = new List<Produto>()
            {
              new Produto{
                CnpjFornecedor = "93.134.039/0001-82",
                CodigoFornecedor = "KJK-DLSKA",
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now,
                Descricao = "fuhsdiuhfds",
                Situacao = "Ativo"
            }
            };
        fakeRepo.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(list));
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ProdutoDTO, Produto>().ReverseMap());
        var map = config.CreateMapper();
        // When
        var produtoManager = new ProdutoManager(fakeRepo.Object, map);
        var result = await produtoManager.GetAllAsync();
        // Then
        Assert.True(result.Success);
        Assert.True(result.ListProdutoDTO.Count() > 0);
    }

    [Fact]
    public async void DeleteAsyncDeveRetornarSituacaoInativo()
    {
        // Given
        var fakeRepo = new Mock<IProdutoRepository>();
        fakeRepo.Setup(x => x.DeleteAsync(4)).Returns(Task.FromResult(true));
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ProdutoDTO, Produto>().ReverseMap());
        var map = config.CreateMapper();
        // When
        var produtoManager = new ProdutoManager(fakeRepo.Object, map);
        var result = await produtoManager.DeleteAsync(4);
        // Then
        Assert.True(result.Success);
    }
}