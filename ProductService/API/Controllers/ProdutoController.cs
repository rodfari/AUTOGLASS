using Application.DTO;
using Application.Ports;
using Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly ILogger<ProdutoController> logger;
    private readonly IProdutoManager produtoManager;

    public ProdutoController(
            ILogger<ProdutoController> logger,
            IProdutoManager produtoManager)
    {
        this.logger = logger;
        this.produtoManager = produtoManager;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var list = await produtoManager.GetAllAsync();
        return Ok(list.ListProdutoDTO);
    }

    [HttpPost]
    public async Task<ActionResult> Post()
    {
        ProdutoRequest request = new()
        {
            ProdutoDTO = new ProdutoDTO
            {
                CnpjFornecedor = "84326487234",
                CodigoFornecedor = "1232143",
                CodigoProduto = 2,
                DataFabricacao = DateTime.Now.AddDays(-10),
                DataValidade = DateTime.Now,
                Descricao = "DESCRICAO"
            }
        };

        await produtoManager.CreateAsync(request);
        return Ok();
    }
}