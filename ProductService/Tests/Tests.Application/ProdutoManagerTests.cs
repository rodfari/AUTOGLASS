using Application;
using Application.DTO;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using Domain.Ports;
using Domain.Structures;
using Moq;
using Tests.Application.Fixture;

namespace Tests.Application;

public class ProdutoManagerTests
{
    [Fact]
    public async void CreateAsyncDeveRetornarSuccess()
    {
        var produto = new Produto();

        var ProdutoRequest = new ProdutoRequest
        {
            ProdutoDTO = ProdutoFixture.DtoInserirProduto
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


    [Theory]
    [InlineData("INATIVO", 0, 10)]
    [InlineData("ATIVO", 0, 10)]
    [InlineData("ATIVO", 1, 10)]
    [InlineData("ATIVO", 2, 10)]
    [InlineData("INATIVO", 1, 2)]
    public async void GetAllAsyncTestaFiltro(string Situacao, int page, int amount)
    {
        // Given
        var fakeRepo = new Mock<IProdutoRepository>();
        

        var returnFilter = ProdutoFixture
        .ListProduto
        .Where(x => x.Situacao.Equals(Situacao))
        .Skip(page < 2 ? 0 : page * amount)
        .Take(amount)
        .ToList();

        fakeRepo.Setup(x => x.GetAllAsync(x => x.Situacao == Situacao, page, amount))
        .Returns(Task.FromResult(returnFilter));
        
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ProdutoDTO, Produto>().ReverseMap());
        var map = config.CreateMapper();
        
        // When
        var produtoManager = new ProdutoManager(fakeRepo.Object, map);
        var result = await produtoManager.GetAllAsync(x => x.Situacao == Situacao, page, amount);
        
        // Then
        Assert.True(result.Success);

        if(Situacao == ProdutoSituacao.INATIVO && page == 0 && amount == 10)
            Assert.Equal(3, result.ListProdutoDTO.Count());

        else if(Situacao == ProdutoSituacao.ATIVO && page == 0 && amount == 10)
            Assert.Equal(7, result.ListProdutoDTO.Count());
        
        else if(Situacao == ProdutoSituacao.ATIVO && page >= 2 && amount == 10)
            Assert.Empty(result.ListProdutoDTO);
        
        else if(Situacao == ProdutoSituacao.INATIVO && page == 1 && amount == 2)
            Assert.Equal(2, result.ListProdutoDTO.Count());
    }

    [Fact]
    public async void DeleteAsyncDeveRetornarVerdadeiroCasoSucesso()
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