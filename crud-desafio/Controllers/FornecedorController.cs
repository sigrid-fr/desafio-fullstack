namespace crud_desafio.Controllers;

using AutoMapper;
using crud_desafio.Models.Fornecedores;
using Microsoft.AspNetCore.Mvc;
using crud_desafio.Services;

[ApiController]
[Route("api/v1/[controller]")]
public class FornecedorController : ControllerBase
{
    private IFornecedorService _fornecedorService;
    private IMapper _mapper;

    public FornecedorController(
        IFornecedorService fornecedorService,
        IMapper mapper)
    {
        _fornecedorService = fornecedorService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var fornecedores = _fornecedorService.GetAll();
        return Ok(fornecedores);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var fornecedor = _fornecedorService.GetById(id);
            return Ok(fornecedor);
        }
        catch (KeyNotFoundException e)
        {
            // Retorna KeyNotFoundException
            return NotFound($"Fornecedor não encontrado: {e.Message}");
        }
        catch (Exception e)
        {
            // Retorna exceção
            Console.WriteLine($"Erro inesperado: {e.Message}");
            return StatusCode(500, "Ocorreu um erro interno no servidor.");
        }
    }

    //Busca por nome
    [HttpGet("por-nome")]
    public IActionResult GetFornecedoresByName([FromQuery] string nome)
    {
        var fornecedores = _fornecedorService.GetFornecedoresByName(nome);
        return Ok(fornecedores);
    }

    //Busca por cnpj/cpf
    [HttpGet("por-cnpjoucpf")]
    public IActionResult GetFornecedoresByCnpjouCpf([FromQuery] string cnpjoucpf)
    {
        var fornecedores = _fornecedorService.GetFornecedoresByCnpjouCpf(cnpjoucpf);
        return Ok(fornecedores);
    }

    [HttpPost]
    public IActionResult Create(CreateRequestFornecedor model)
    {
        //validação de CEP
       var cepInfo = _fornecedorService.ValidateCep(model.Cep);
        
        if (cepInfo == null)
        {
            return BadRequest("Cep inválido ou não encontrado!");
        }

        //Caso o Cep seja válido, o cadastro segue normalmente
        _fornecedorService.Create(model);
        return Ok(new { message = "O fornecedor foi criado!" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRequestFornecedor model)
    {
        _fornecedorService.Update(id, model);
        return Ok(new { message = "O fornecedor foi atualizado!" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _fornecedorService.Delete(id);
        return Ok(new { message = "O fornecedor foi excluído!" });
    }
}