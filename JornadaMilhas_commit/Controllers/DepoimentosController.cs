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
        var depoimento = _context.Depoimentos.FirstOrDefault(depoimentoRecuperado => depoimentoRecuperado.Id == id);
        var depoimentoDto = _mapper.Map<ReadDepoimentoDto>(depoimento);
        if (depoimentoDto == null) return NotFound();
        return Ok(depoimentoDto);
    }

    [HttpGet]
    public IEnumerable<ReadDepoimentoDto> RecuperarTodosDepoimentos(
        [FromQuery] int skip = 0,[FromQuery] int take = 50)
    {
        return _mapper.Map< List<ReadDepoimentoDto>>(_context.Depoimentos.Skip(skip).Take(take));
    }
    [HttpPut("{id}")]
    public IActionResult AtualizaDepoimentos([FromRoute] int id,
        [FromBody] UpdateDepoimentoDto depoimentoAtualizarDto)
    {
        var depoimento = _context.Depoimentos.FirstOrDefault(depoimentoRecuperado => depoimentoRecuperado.Id == id);
        if (depoimento == null) return NotFound();
        _mapper.Map(depoimentoAtualizarDto,depoimento);
        _context.SaveChanges();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult DeletaDepoimentos([FromRoute] int id)
    {
        var depoimento = _context.Depoimentos.FirstOrDefault(depoimentoRecuperado => depoimentoRecuperado.Id == id);
        if (depoimento == null) return NotFound();
        _context.Remove(depoimento);
        _context.SaveChanges();
        return NoContent();

    }

    //[HttpGet("[controller]-home")]
    //public IEnumerable<Depoimentos> RecuperaDepoimentosAleatorio()
    //{
    //    int[] seq = new int[3] { 1,2,3};
    //    int numeroAleatorioAtual;
    //    List<int> numeroAleatorioAnterior = new List<int>();
    //    List<Depoimentos> depoimentosAleatorios = new List<Depoimentos>();
    //    Depoimentos dep;
    //    int elementsNumber = _context.Depoimentos.Count();
    //    while (depoimentosAleatorios.Count != seq.Length)
    //    {
    //        numeroAleatorioAtual = RandomNumberGenerator.GetInt32(elementsNumber+1);
    //        if (numeroAleatorioAtual <= elementsNumber && numeroAleatorioAtual > 0)
    //        {
    //            depoimentosAleatorios.Add(_context.Depoimentos.FirstOrDefault(dep => dep.Id == numeroAleatorioAtual));
    //            Console.WriteLine(numeroAleatorioAtual);
    //        }
                

    //    }

    //    return depoimentosAleatorios;
    //}
}
