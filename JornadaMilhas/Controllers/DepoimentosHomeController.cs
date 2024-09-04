using JornadaMilhas.Data;
using JornadaMilhas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using JornadaMilhas.Controllers;
using JornadaMilhas.Data.Dtos.DepoimentosDtos;
using AutoMapper;

namespace JornadaMilhas.Controllers;

[Route("/Depoimentos-home")]
[ApiController]
public class DepoimentosHomeController : ControllerBase
{
    public JornadaMilhasContext _context;

    public DepoimentosHomeController(JornadaMilhasContext context)
    {
        _context = context;
    }
    [HttpGet]
    public ActionResult<List<Depoimentos>> RecuperaDepoimentosAleatorio()
    {
        List<Depoimentos> ListaDepoimentos = _context.Depoimentos.ToList();
        List<Depoimentos> depoimentosAleatorios = new List<Depoimentos>(3);
        Depoimentos depoimentoAleatorio = new Depoimentos();

        int contador = 1;
        var mensagemSemDepoimentosSuficientes = new { 
            Mensagem = "Não há depoimentos suficientes" 
        };
        int numeroDepoimentos = ListaDepoimentos.Count();
        List<int> numeroAleatorioIteracaoAnterior = new List<int>();
        int numeroAleatorioIteracaoAtual;

        if (numeroDepoimentos < 1)
            return NotFound(mensagemSemDepoimentosSuficientes);
        else if (numeroDepoimentos <= 3 &&
            numeroDepoimentos > 1)
            return Ok(ListaDepoimentos);
        
        while (depoimentosAleatorios.Count < 3)
        {
            numeroAleatorioIteracaoAtual = RandomNumberGenerator
                                    .GetInt32(numeroDepoimentos);
            depoimentoAleatorio = ListaDepoimentos
                .FirstOrDefault( dep => dep.Id == numeroAleatorioIteracaoAtual);

            if (depoimentoAleatorio != null 
                && !numeroAleatorioIteracaoAnterior.Contains(numeroAleatorioIteracaoAtual))
            {
                depoimentosAleatorios.Add(depoimentoAleatorio);
            }

            contador++;
            numeroAleatorioIteracaoAnterior.Add(numeroAleatorioIteracaoAtual);
        }

        return Ok(depoimentosAleatorios);
    }
}
