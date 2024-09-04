using JornadaMilhas.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace JornadaMilhas.Teste.Integracao.Api;
public class JornadaMilhasWebApplicationFactory : WebApplicationFactory<Program>
{
    public JornadaMilhasContext Context { get;  }
    private IServiceScope scope;
    private IConfiguration Configuration;
    public JornadaMilhasWebApplicationFactory()
    {
        Configuration = new ConfigurationBuilder()
            .AddUserSecrets<JornadaMilhasWebApplicationFactory>()
            .Build();
        this.scope = Services.CreateScope();
        Context = scope.ServiceProvider.GetRequiredService<JornadaMilhasContext>();
        
    }


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var connectionString = Configuration["connectionString"];
        builder.ConfigureServices(
            services =>
        {
            services.RemoveAll(typeof(DbContextOptions<JornadaMilhasContext>));
            services.AddDbContext<JornadaMilhasContext>(option => option
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        });
        base.ConfigureWebHost(builder);
    }
}
