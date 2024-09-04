using JornadaMilhas.Data;
using JornadaMilhas.Data.Dtos.DepoimentosDtos;
using JornadaMilhas.Models;
using JornadaMilhas.Teste.Integracao.Api.DataBuilders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Teste.Integracao.Api.ControllersTest;
[Collection(nameof(ContextoCollection))]
public class JornadaMilhasDepoimentosCrud : IDisposable
{
    private readonly JornadaMilhasWebApplicationFactory app;

    public JornadaMilhasDepoimentosCrud(JornadaMilhasWebApplicationFactory app)
    {
        this.app = app;
    }

    [Fact]
    public async Task RetornaStatusQuandoFeitoPost() 
    {
        //Arrange
        var depoimentoFake = new DepoimentosDataBuilder().Generate();
       
        using var client = app.CreateClient();
        //Act
        var result = await client
            .PostAsJsonAsync<Depoimentos>("/Depoimentos", depoimentoFake);
        //Assert
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }
    [Fact]
    public async Task RetornaRetornaStatusOKQuandoFeitoGetPeloId()
    {
        //Arrange

        using var client = app.CreateClient();
        var depoimentoDto = new CreateDepoimentoDto()
        {
            Nome = "Jean",
            Depoimento = "Teste",
            Foto = "newadkfksjkfjsdjflkdlsfkl"
        };
        var postResult = await client
            .PostAsJsonAsync("/Depoimentos", depoimentoDto);
        string pathPostContentLocation = postResult
            .Headers.Location.PathAndQuery;
        //Act
        var getResult = await client.GetAsync(pathPostContentLocation);
        //Assert
        Assert.Equal(HttpStatusCode.OK, getResult.StatusCode);
    }

    [Fact]
    public async Task RetornaStatusNuloQuandoGetDoDepoimentoNaoExiste()
    {
        //Arrange

        int idInvalido = -99;
        using var client = app.CreateClient();
        //Act
        var result = await client.GetAsync($"/Depoimentos/{idInvalido}");
        //Assert
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task RetornaRetornaStatusOKQuandoFeitoUpdatePeloId()
    {
        //Arrange
        using var client = app.CreateClient();
        var depoimentoFake = new DepoimentosDataBuilder().Generate();
        var postResult = await client
            .PostAsJsonAsync("/Depoimentos", depoimentoFake);
        string pathPostContentLocation = postResult.Headers
                                        .Location.PathAndQuery;

        //Act
        depoimentoFake.Depoimento = "Bla bla bla";

        var putResult = await client.PutAsJsonAsync(
            pathPostContentLocation,
            depoimentoFake);
       
        //Assert
        Assert.Equal(HttpStatusCode.NoContent, putResult.StatusCode);
    }

    [Fact]
    public async Task RetornaRetornaStatusOKQuandoFeitoDeletePeloId()
    {
        //Arrange
        using var client = app.CreateClient();
        var depoimentoFake = new DepoimentosDataBuilder().Generate();
        var postResult = await client
            .PostAsJsonAsync("/Depoimentos", depoimentoFake);
        string pathPostContentLocation = postResult
            .Headers.Location.PathAndQuery;
        //Act
        var deleteResult = await client.DeleteAsync(pathPostContentLocation);

        //Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResult.StatusCode);
    }

    [Fact]
    public async Task RetornaRetornaStatusNotFoundQuandoFeitoDeletePorIdInexistente()
    {
        //Arrange
        //var app = new JornadaMilhasWebApplicationFactory();
        using var client = app.CreateClient();
        //Act
        int idInexistente = -99;
        var deleteResult = await client
            .DeleteAsync($"/Depoimentos/{idInexistente}");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, deleteResult.StatusCode);
    }

    [Fact]
    public async Task RetornaRetornaStatusNotFoundQuandoFeitoUpdatePorIdInexistente()
    {
        //Arrange
        //var app = new JornadaMilhasWebApplicationFactory();
        using var client = app.CreateClient();
        var depoimentoFake = new DepoimentosDataBuilder().Generate();
        int idInexistente = -99;
        //Act
        var putResult = await client.
            PutAsJsonAsync(
                $"/Depoimentos/{idInexistente}",
                depoimentoFake);

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, putResult.StatusCode);
    }

    [Fact]
    public async Task RetornaComparacaoElementosConsultaPaginada()
    {
        //Arrange
        List<Depoimentos> listaDepoimentosFake = new DepoimentosDataBuilder()
                                                     .Generate(100);
        using var client = app.CreateClient();
        app.Context.AddRange(listaDepoimentosFake);
        app.Context.SaveChanges();

        int pular = 20;
        int pegar = 80;

        //Act
        
        var getResult = await client
            .GetFromJsonAsync<ICollection<Depoimentos>>(
             $"/Depoimentos?skip={pular}&take={pegar}");

        //Assert
        Assert.Equal(pegar, getResult.Count);
    }

    [Fact]
    public async Task RetornaComparacaoElementosConsultaPaginadaTakeNegativo()
    {
        //Arrange
        app.Context.Database.ExecuteSqlRaw("DELETE FROM Depoimentos");
        List<Depoimentos> listaDepoimentosFake = new DepoimentosDataBuilder()
                                                     .Generate(100);
        using var client = app.CreateClient();
        app.Context.AddRange(listaDepoimentosFake);
        app.Context.SaveChanges();

        int pular = 20;
        int pegar = -9;

        //Act  + Assert
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            var getResult = await client
            .GetFromJsonAsync<ICollection<Depoimentos>>(
                $"/Depoimentos?skip={pular}&take={pegar}");
        });
    }
    public void Dispose()
    {
        app.Context.Database.ExecuteSqlRaw("DELETE FROM Depoimentos");
        app.Context.Database
            .ExecuteSqlRaw("ALTER TABLE Depoimentos AUTO_INCREMENT = 1");
    }
}
