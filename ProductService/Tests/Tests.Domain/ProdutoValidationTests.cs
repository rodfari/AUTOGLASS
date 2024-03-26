using Domain.Entities;
using Domain.Exceptions;

namespace Tests.Domain;

public class ProdutoValidationTests
{
    [Fact]
    public void DataFabricaoMenorQueDataValidadeEDescricaoPreenchido()
    {
        Produto produto = new Produto
        {
            DataFabricacao = new DateTime(2024, 08, 01),
            DataValidade = new DateTime(2024, 09, 01),
            Descricao = "Teste"
        };

        Assert.True(produto.DataFabricacao < produto.DataValidade);
    }

    [Fact]
    public void ValidaTipoDeExcecaoQuandoDataFabricacaoMaioQueDataValidade()
    {
        try
        {
            // Given
            Produto produto = new Produto
            {
                DataFabricacao = new DateTime(2024, 010, 01),
                DataValidade = new DateTime(2024, 09, 01),
                Descricao = "Teste"
            };
            // When
            produto.Validate();
        }
        catch (Exception ex)
        {
            // Then
            Assert.IsType<ExpireDateException>(ex);
            Assert.True(!string.IsNullOrEmpty(ex.Message));
        }
    }
    
    [Fact]
    public void ValidaTipoDeExcecaoQuandoDescricaoNaoPreenchida()
    {
        try
        {
            // Given
            Produto produto = new Produto
            {
                DataFabricacao = new DateTime(2024, 08, 01),
                DataValidade = new DateTime(2024, 09, 01)
            };
            // When
            produto.Validate();
        }
        catch (Exception ex)
        {
            // Then
            Assert.IsType<NotNullOrEmptyStringException>(ex);
            Assert.True(!string.IsNullOrEmpty(ex.Message));
        }
    }
}