namespace Application.DTO;

public class ProdutoDTO
{
    public int CodigoProduto { get; set; }
    public string Descricao { get; set; }
    public string Situacao { get; set; }
    public DateTime DataFabricacao { get; set; }
    public DateTime DataValidade { get; set; }
    public string CodigoFornecedor { get; set; }
    public string CnpjFornecedor { get; set; }
}