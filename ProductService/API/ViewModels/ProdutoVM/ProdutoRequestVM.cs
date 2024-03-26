using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.ProdutoVM;

public class ProdutoRequestVM: ProdutoBaseVM
{
    [Required]
    public string Descricao { get; set; }
    public string Situacao { get; set; }
    public DateTime DataFabricacao { get; set; }
    public DateTime DataValidade { get; set; }
    public string CodigoFornecedor { get; set; }
    public string CnpjFornecedor { get; set; }
}