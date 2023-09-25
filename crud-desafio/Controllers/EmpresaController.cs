namespace crud_desafio.Controllers;

using AutoMapper;
using crud_desafio.Models.Empresas;
using Microsoft.AspNetCore.Mvc;
using crud_desafio.Services;

[ApiController]
[Route("api/v1/[controller]")]
public class EmpresaController : ControllerBase
{
    private IEmpresaService _empresaService;
    private IMapper _mapper;

    public EmpresaController(
        IEmpresaService empresaService,
        IMapper mapper)
    {
        _empresaService = empresaService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var empresas = _empresaService.GetAll();
        return Ok(empresas);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var empresa = _empresaService.GetById(id);
            return Ok(empresa);
        }
        catch (KeyNotFoundException e)
        {
            // Retorna KeyNotFoundException
            return NotFound($"Empresa não encontrada: {e.Message}");
        }
        catch (Exception e)
        {
            // Retorna exceção
            Console.WriteLine($"Erro inesperado: {e.Message}");
            return StatusCode(500, "Ocorreu um erro interno no servidor.");
        }
    }

    [HttpPost]
    public IActionResult Create(CreateRequestEmpresa model)
    {
        _empresaService.Create(model);
        return Ok(new { message = "A empresa foi criada!" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRequestEmpresa model)
    {
        _empresaService.Update(id, model);
        return Ok(new { message = "A empresa foi atualizada!" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _empresaService.Delete(id);
        return Ok(new { message = "A empresa foi excluída!" });
    }
}