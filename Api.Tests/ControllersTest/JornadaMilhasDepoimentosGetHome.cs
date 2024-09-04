using JornadaMilhas.Models;
using JornadaMilhas.Teste.Integracao.Api.DataBuilders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JornadaMilhas.Teste.Integracao.Api.ControllersTest;
[Collection(nameof(ContextoCollection))]
public class JornadaMilhasDepoimentosGetHome : IDisposable
{
    private readonly JornadaMilhasWebApplicationFactory app;

    public JornadaMilhasDepoimentosGetHome(JornadaMilhasWebApplicationFactory app)
    {
        this.app = app;
    }

    public void Dispose()
    {
        app.Context.Database.ExecuteSqlRaw("DELETE FROM Depoimentos");
        app.Context.Database
            .ExecuteSqlRaw("ALTER TABLE Depoimentos AUTO_INCREMENT = 1");
    }

    [Fact]  
    public async Task RetornaStatusOkQuandoFeitoGetDepoimentosAleatorios()
    {
        //Assert
        var client = app.CreateClient();
        //Act
        var getResult = await client.GetAsync("/Depoimentos-home");
        //Assert
        Assert.Equal(HttpStatusCode.OK, getResult.StatusCode);
    }
    [Fact]
    public async Task 
        RetornaConfirmacaoListaComTresElementosQuandoFeitoGetDepoimentosAleatorios()
    {
        //Assert
        app.Context.Database.ExecuteSqlRaw("DELETE FROM Depoimentos");
        app.Context.Database
            .ExecuteSqlRaw("ALTER TABLE Depoimentos AUTO_INCREMENT = 1");
        List<Depoimentos> listaDepoimentosFake = new DepoimentosDataBuilder()
                                                     .Generate(9);
        using var client = app.CreateClient();
        app.Context.AddRange(listaDepoimentosFake);
        app.Context.SaveChanges();


        //Act
        var getResult = await client
            .GetFromJsonAsync<ICollection<Destinos>>("/Depoimentos-home");
        

        //Assert
        Assert.Equal(3, getResult.Count);
    }

}
