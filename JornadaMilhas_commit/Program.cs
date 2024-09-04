using JornadaMilhas.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Add Cors
builder.Services.AddCors();
//Building a context with Depoimentosconnection string
var connectionString = builder.Configuration.GetConnectionString("DepoimentoConnection");
//If the test has been connected, not connect
//builder.Services.AddDbContext<DepoimentosContext>(opt =>
//{ 
//    if(!opt.IsConfigured)
//    {
//        opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//    }
//});
builder.Services.AddDbContext<JornadaMilhasContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//Building automapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add builder for newtonsoft Json
builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Cors permission

app.UseCors( c => 
    c.AllowAnyOrigin().
    AllowAnyHeader().
    AllowAnyMethod()
    );

app.UseAuthorization();

app.MapControllers();

app.Run();

//Integration test
public partial class Program { }