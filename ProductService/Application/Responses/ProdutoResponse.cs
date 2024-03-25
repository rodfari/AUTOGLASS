using Application.Base;
using Application.DTO;

namespace Application.Responses;

public class ProdutoResponse: Response
{
    public ProdutoDTO ProdutoDTO { get; set; }
    public List<ProdutoDTO> ListProdutoDTO { get; set; }
}