using Domain.Exceptions;

namespace Domain.Entities;
public class Produto
{
    public int CodigoProduto { get; set; }
    public string Descricao { get; set; }
    public string Situacao { get; set; }
    public DateTime DataFabricacao { get; set; }
    public DateTime DataValidade { get; set; }
    public string CodigoFornecedor { get; set; }
    public string CnpjFornecedor { get; set; }

    public void Validate(){
        if (DataValidade <= DataFabricacao)
        {
            throw new ExpireDateException("A data de fabricação não pode ser superior ou igual a data de vencimento");
        }
        if(string.IsNullOrEmpty(Descricao))
        {
            throw new NotNullOrEmptyStringException("A descrição do produto deve contem um valor");
        }
    }
}
