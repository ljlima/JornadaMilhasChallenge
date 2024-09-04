using AutoMapper;
using JornadaMilhas.Controllers;
using JornadaMilhas.Data;
using JornadaMilhas.Data.Dtos;
using JornadaMilhas.Models;
using JornadaMilhas.Teste.Integracao.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Tests.ControllersTest;
public class DatabaseFixture
{
    public JornadaMilhasContext Context { get; private set; }
    private IConfiguration Configuration;
    public DatabaseFixture()
    {
        Configuration = new ConfigurationBuilder()
            .AddUserSecrets< DatabaseFixture>()
            .Build();
        string connectionString = Configuration["connectionString"];
        var option = new DbContextOptionsBuilder<JornadaMilhasContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
        Context = new JornadaMilhasContext(option);
    }

    public void DbDispose()
    {
        //Delete all data from JornadaMilhas Database
        Context.Database.ExecuteSqlRaw("DELETE FROM JornadaMilhas");
    }

    
}
