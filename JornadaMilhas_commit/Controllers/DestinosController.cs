using AutoMapper;
using JornadaMilhas.Data;
using JornadaMilhas.Data.Dtos.DestinosDtos;
using JornadaMilhas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Completions;
using OpenAI_API;
using OpenAI_API.Chat;
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using OpenAI_API.Moderation;

namespace JornadaMilhas.Controllers;

[Route("[controller]")]
[ApiController]
public class DestinosController : ControllerBase
{
    private readonly JornadaMilhasContext context;
    private readonly IMapper mapper;
    public string OPENAI_API_KEY = "YOU_API_KEY";
    public DestinosController(JornadaMilhasContext context, 
        IMapper _mapper)
    {
        this.context = context;
        mapper = _mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionaDestino([FromBody] CreateDestinoDto destinoDto) 
    {
        var destino = mapper.Map<Destinos>(destinoDto);
        var nomeDestino = destino.Nome;
        if (destino.TextoDescritivo == null || destino.TextoDescritivo == " ")
        {
            var respostaGpt = await PesquisaTextoDescritivo(nomeDestino);
            destino.TextoDescritivo = respostaGpt;
        }
            
        context.Add(destino);
        context.SaveChanges();
        if (destino == null) return NotFound();
        return CreatedAtAction(
            nameof(RecuperaDestinoPorId),
            new { id = destino.Id },
            destino);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaDestinoPorId([FromRoute] int id)
    {
        var destinoDto = mapper.Map<ReadDestinosDto>(context
            .Destinos
            .FirstOrDefault(
                destinoProcurado => destinoProcurado.Id == id
            ));
            
        if (destinoDto == null) return NotFound();
        return Ok(destinoDto);
    }
    [HttpPut("{id}")]
    public IActionResult AtualizaDestino([FromRoute] int id, UpdateDestinosDto destinoDto)
    {
        var destino = mapper.Map<Destinos>(
            context.Destinos
            .FirstOrDefault(
                destinoProcurado => 
                destinoProcurado.Id == id));
        if (destino == null) return NotFound();
        context.Update(destino);
        context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaDestino([FromRoute] int id)
    {
        var destino = context
            .Destinos
            .FirstOrDefault(destinoProcurado => destinoProcurado.Id == id);
        if (destino == null) return NotFound();
        context.Remove(destino);
        context.SaveChanges();
        return NoContent();
    }

    [HttpGet]
    public IEnumerable<Destinos> RecuperaMuitos(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 50)
    {
        var destinos = context.Destinos.Skip(skip).Take(take);
        return destinos;
    }
    [HttpGet("/Destinos/destinos")]

    public ActionResult<List<Destinos>> RecuperaMuitosPorNome(
        [FromQuery] string nome)
    {
        var mensagem = new { mensagem = "Nenhum destino foi encontrado" };
 
        var destinos = context.Destinos.Where(dest => dest.Nome == nome).OrderBy(dest => dest.Id);
        if (!destinos.Any()) return NotFound(mensagem);
        return Ok(destinos);
    }
    [HttpGet("/api/GepRequest")]
    public async Task<string> PesquisaTextoDescritivo(string nomeDestino)
    {
        string query = $"Faça um resumo sobre {nomeDestino} enfatizando " +
            "o porque este lugar é incrível. Utilize uma linguagem" +
            " informal e até 100 caracteres no máximo em cada parágrafo. " +
            "Crie 2 parágrafos neste resumo.";
        string outputResult = "";
        var openai = new OpenAIAPI(OPENAI_API_KEY);
        CompletionRequest completionRequest = new CompletionRequest();
        completionRequest.Prompt = query;
        completionRequest.Model = OpenAI_API.Models.Model.DefaultModel;
        completionRequest.MaxTokens = 1024;

        var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

        foreach (var completion in completions.Completions)
        {
            outputResult += completion.Text;
        }
        
        return outputResult;
    }
  

}