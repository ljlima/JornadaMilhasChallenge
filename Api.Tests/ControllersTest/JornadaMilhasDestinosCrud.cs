using Bogus;
using JornadaMilhas.Data.Dtos;
using JornadaMilhas.Data.Dtos.DestinosDtos;
using JornadaMilhas.Models;
using JornadaMilhas.Teste.Integracao.Api.DataBuilders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Teste.Integracao.Api.ControllersTest;
[Collection(nameof(ContextoCollection))]
public class JornadaMilhasDestinosCrud : IDisposable
{
    private readonly JornadaMilhasWebApplicationFactory app;

    public JornadaMilhasDestinosCrud(JornadaMilhasWebApplicationFactory app)
    {
        this.app = app;
    }

    public void Dispose()
    {
        app.Context.Database.ExecuteSqlRaw("DELETE FROM Destinos");
        app.Context.Database
            .ExecuteSqlRaw("ALTER TABLE Destinos AUTO_INCREMENT = 1");
    }

    [Fact]
    public async Task RetornaStatusOkQuandoFeitoPostDestino ()
    {
        //Arrange
        var destinoFake = new DestinoDataBuilder().Generate();

        //Act
        using var client = app.CreateClient();
        var result = await client.PostAsJsonAsync("/Destinos", destinoFake);
        //Assert
        Assert.Equal(HttpStatusCode.Created, result.StatusCode); 
    }

    [Fact]
    public async Task RetornaStatusOkQuandoFeitoGetDestinoPorId()
    {
        //Arrange
        var destinoFake = new DestinoDataBuilder().Generate();

        //Act
        using var client = app.CreateClient();
        var postResult = await client
            .PostAsJsonAsync("/Destinos", destinoFake);
        string pathPostContentLocation = postResult.Headers.Location.PathAndQuery;
        var getResult = await client.GetAsync(postResult.Headers.Location.PathAndQuery);

        //Assert
        Assert.Equal(HttpStatusCode.OK, getResult.StatusCode);
    }


    [Fact]
    public async Task RetornaStatusOkQuandoFeitoUpdatePorId()
    {
        //Arrange
        var destinoPostFake = new DestinoDataBuilder().Generate();
        var destinoUpdateFake = new DestinoDataBuilder().Generate();
        using var client = app.CreateClient();
        var postResult = await client
            .PostAsJsonAsync("/Destinos", destinoPostFake);
        string pathPostContentLocation = postResult.Headers.Location.PathAndQuery;

        //Act
        var putResult = await client
            .PutAsJsonAsync(pathPostContentLocation,destinoUpdateFake);
        

        //Assert
        Assert.Equal(HttpStatusCode.NoContent, putResult.StatusCode);
    }

    [Fact]
    public async Task RetornaStatusOkQuandoFeitoDeletePorId()
    {
        //Arrange
        var destinoFake = new DestinoDataBuilder().Generate();
        
        using var client = app.CreateClient();
        var postResult = await client
            .PostAsJsonAsync("/Destinos", destinoFake);
        string pathPostContentLocation = postResult.Headers.Location.PathAndQuery;

        //Act
        var deleteResult = await client
            .DeleteAsync(pathPostContentLocation);


        //Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResult.StatusCode);
    }
    [Fact]
    public async Task RetornaStatusOkQuandoFeitoGetMany()
    {
        //Arrange
        using var client = app.CreateClient();

        //Act
        var getManyResult = await client.GetAsync("/Destinos");


        //Assert
        Assert.Equal(HttpStatusCode.OK, getManyResult.StatusCode);
    }

    [Fact]
    public async Task RetornaStatusNotFoundQuandoFeitoGetDestinoInexistente()
    {
        //Arrange
        int destinoInexistente = -99;


        //Act
        using var client = app.CreateClient();  
        var getResult = await client
            .GetAsync($"/Destinos/{destinoInexistente}");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, getResult.StatusCode);
    }

    [Fact]
    public async Task RetornaStatusNotFoundQuandoFeitoUpdateDestinoInexistente()
    {
        //Arrange
        int idDestinoInexistente = -99;
        var destinoFake = new DestinoDataBuilder().Generate();
        //Act
        using var client = app.CreateClient();
        var putResult = await client
            .PutAsJsonAsync($"/Destinos/{idDestinoInexistente}",destinoFake);

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, putResult.StatusCode);
    }

    [Fact]
    public async Task RetornaStatusNotFoundQuandoFeitoDeleteDestinoInexistente()
    {
        //Arrange
        int destinoInexistente = -99;

        //Act
        using var client = app.CreateClient();
        var deleteResult = await client
            .DeleteAsync($"/Destinos/{destinoInexistente}");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, deleteResult.StatusCode);
    }
    [Fact]
    public async Task RetornaStatusNotFoundMensagemQuandoFeitoGetDestinoNomeInexistente()
    {
        //Arrange
        var fake = new Faker<string>();
        var destinoFake = fake.CustomInstantiator(
            f => 
            {
                var nomeDestinoFake = f.Lorem.Sentence(1);
                return nomeDestinoFake;
            });

        //Act
        using var client = app.CreateClient();
        var getResult = await client
            .GetAsync($"/Destinos/destinos?nome={destinoFake}");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, getResult.StatusCode);
    }

    [Fact]
    public async Task RetornaStatusOkQuandoFeitoGetDestinoComTextoDescricaoNuloOuVazio()
    {
        //Arrange
        var destinoFake = new DestinoDataBuilder().Generate();

        //Act
        using var client = app.CreateClient();
        var postResult = await client
            .PostAsJsonAsync("/Destinos", destinoFake);
        string pathPostContentLocation = postResult.Headers.Location.PathAndQuery;
        var getResult = await client
            .GetAsync(postResult.Headers.Location.PathAndQuery);

        //Assert
        Assert.Equal(HttpStatusCode.OK, getResult.StatusCode);

    }

    
}
