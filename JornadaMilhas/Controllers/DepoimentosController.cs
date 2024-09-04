using AutoMapper;
using JornadaMilhas.Data;
using JornadaMilhas.Data.Dtos.DepoimentosDtos;
using JornadaMilhas.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Security.Cryptography;

namespace JornadaMilhas.Controllers;


[ApiController]
[Route("[controller]")]

public class DepoimentosController : ControllerBase
{
    private JornadaMilhasContext _context;
    private IMapper _mapper;

    public DepoimentosController(JornadaMilhasContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionarDepoimento([FromBody] CreateDepoimentoDto depoimentoDto)
    {
        var depoimento = _mapper.Map<Depoimentos>(depoimentoDto);
        _context.Add(depoimento);
        _context.SaveChanges();

        return CreatedAtAction(nameof(RecuperarDepoimento), 
            new{ id = depoimento.Id },
            depoimento);
    }

    [HttpGet("{id}")]
    public IActionResult? RecuperarDepoimento([FromRoute] int id)
    {
        var depoimento = _context.Depoimentos
            .FirstOrDefault(depoimentoRecuperado => depoimentoRecuperado.Id == id);
        var depoimentoDto = _mapper.Map<ReadDepoimentoDto>(depoimento);
        if (depoimentoDto == null) return NotFound();
        return Ok(depoimentoDto);
    }

    [HttpGet]
    public IEnumerable<ReadDepoimentoDto> RecuperarTodosDepoimentos(
        [FromQuery] int skip = 0,[FromQuery] int take = 50)
    {
        return _mapper
            .Map<List<ReadDepoimentoDto>>(_context.Depoimentos
            .Skip(skip)
            .Take(take));
    }
    [HttpPut("{id}")]
    public IActionResult AtualizaDepoimentos([FromRoute] int id,
        [FromBody] UpdateDepoimentoDto depoimentoAtualizarDto)
    {
        var depoimento = _context.Depoimentos
            .FirstOrDefault(depoimentoRecuperado => depoimentoRecuperado.Id == id);
        if (depoimento == null) return NotFound();
        _mapper.Map(depoimentoAtualizarDto,depoimento);
        _context.SaveChanges();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult DeletaDepoimentos([FromRoute] int id)
    {
        var depoimento = _context.Depoimentos
            .FirstOrDefault(depoimentoRecuperado => depoimentoRecuperado.Id == id);
        if (depoimento == null) return NotFound();
        _context.Remove(depoimento);
        _context.SaveChanges();
        return NoContent();

    }


}
