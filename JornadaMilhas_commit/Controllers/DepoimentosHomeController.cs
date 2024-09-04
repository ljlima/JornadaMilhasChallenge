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
        
        int contador = 1;
        int numeroAleatorioAtual;
        var nullDepoimentos = new { Mensagem = "Não há depoimentos suficientes" };
        List<int> numeroAleatorioAnterior = new List<int>();
        List<Depoimentos> depoimentosAleatorios = new List<Depoimentos>();
        var ListaDepoimentos = _context.Depoimentos;
        int elementsNumber = ListaDepoimentos.Count();
        Depoimentos depoimentoAleatorio = new Depoimentos();

        if (elementsNumber <= 3)
            return Ok(ListaDepoimentos);

        while (depoimentosAleatorios.Count < 3)
        {
            numeroAleatorioAtual = RandomNumberGenerator
                                    .GetInt32(elementsNumber + 1);

            depoimentoAleatorio = ListaDepoimentos.FirstOrDefault(dep => dep.Id == numeroAleatorioAtual);

            if (depoimentoAleatorio != null && !numeroAleatorioAnterior.Contains(numeroAleatorioAtual))
            {
                depoimentosAleatorios.Add(ListaDepoimentos.FirstOrDefault(dep => dep.Id == numeroAleatorioAtual));
            }

            contador++;
            numeroAleatorioAnterior.Add(numeroAleatorioAtual);
        }

        return Ok(depoimentosAleatorios);
    }
}
