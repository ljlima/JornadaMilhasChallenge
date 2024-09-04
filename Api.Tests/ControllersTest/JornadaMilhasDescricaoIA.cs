using JornadaMilhas.Data.Dtos.DestinosDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Teste.Integracao.Api.ControllersTest;
[Collection(nameof(ContextoCollection))]
public class JornadaMilhasDescricaoIA : IDisposable
{
    private readonly JornadaMilhasWebApplicationFactory app;

    public JornadaMilhasDescricaoIA(JornadaMilhasWebApplicationFactory app)
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
    public async Task 
        RetornaRetornaTextoDescritivoDiferenteDeNuloQuandoFeitoGetComTextoDescricaoVazioOuNulo()
    {
        //Arrange
        var client = app.CreateClient();
        var destinoSemDescricao = new CreateDestinoDto("Conheça Cuiabá", "Foto1",
            "Foto2", 100.00)
        {
            Meta = "TesteMeta",
            TextoDescritivo = " "
        };
        //Act
        var postResult = await client.PostAsJsonAsync<CreateDestinoDto>(
            "/Destinos", destinoSemDescricao);
        string pathPostContentLocation = postResult.Headers.Location.PathAndQuery;
        var getResult = await client.GetAsync(pathPostContentLocation);
        string getContent = await getResult.Content.ReadAsStringAsync();
        //Assert
        Assert.NotEqual(destinoSemDescricao.TextoDescritivo, getContent);
    }

}
