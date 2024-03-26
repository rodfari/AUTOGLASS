using System.Net;
using API.ViewModels.ProdutoVM;
using Application.DTO;
using Application.Ports;
using Application.Requests;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly ILogger<ProdutoController> logger;
    private readonly IProdutoManager produtoManager;
    private readonly IMapper mapper;

    public ProdutoController(
            ILogger<ProdutoController> logger,
            IProdutoManager produtoManager,
            IMapper mapper)
    {
        this.logger = logger;
        this.produtoManager = produtoManager;
        this.mapper = mapper;
    }

    

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoResponseVM>> Get(int id)
    {
        var prod = await produtoManager.GetByIdAsync(id);
        if (!prod.Success)
        {
            return NotFound(new { Messsge = prod.Message });
        }
        var res = mapper.Map<ProdutoResponseVM>(prod.ProdutoDTO);
        return Ok(res);
    }



    [HttpGet]
    public async Task<ActionResult<List<ProdutoResponseVM>>> Filtrar(
        string cnpj, int page = 0, int amount = 10, string situacao = "ATIVO")
    {   

        var list = await produtoManager
            .GetAllAsync(
                x => x.Situacao.Equals(situacao) 
                && !string.IsNullOrEmpty(cnpj)
                && x.CnpjFornecedor.Contains(cnpj) 
                || string.IsNullOrEmpty(cnpj), 
                page, amount);

        var res = mapper.Map<List<ProdutoRequestVM>>(list.ListProdutoDTO);
        return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult> Post(ProdutoRequestVM requestVM)
    {
        ProdutoRequest request = new()
        {
            ProdutoDTO = mapper.Map<ProdutoDTO>(requestVM)
        };
        var res = await produtoManager.CreateAsync(request);
        
        if (res.Success)
        {
            var result = new ObjectResult("Produto criado com seucesso")
            {
                StatusCode = (int)HttpStatusCode.Created
            };
            return result;
        }
        return BadRequest(res.Message);
    }

    [HttpPut]
    public async Task<ActionResult> Put(ProdutoRequestVM requestVM)
    {
        ProdutoRequest request = new()
        {
            ProdutoDTO = mapper.Map<ProdutoDTO>(requestVM)
        };
        var res = await produtoManager.CreateAsync(request);
        
        if (res.Success)
        {
            var result = new ObjectResult("Atualizacao realizada!");
            result.StatusCode = 201;
            return result;
        }
        return BadRequest(res.Message);
    }


    [HttpDelete("{codigoProduto}")]
    public async Task<ActionResult> Delete(int codigoProduto)
    {
        var res = await produtoManager.DeleteAsync(codigoProduto);
        if (res.Success)
        {
            return Ok("Produto excluido com sucesso!");
        }
        return BadRequest(res.Message);
    }
}