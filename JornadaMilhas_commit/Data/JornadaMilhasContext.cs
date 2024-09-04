using JornadaMilhas.Models;
using Microsoft.EntityFrameworkCore;

namespace JornadaMilhas.Data;

public class JornadaMilhasContext : DbContext
{
    public JornadaMilhasContext(DbContextOptions<JornadaMilhasContext> opt) : base(opt) 
    { 

    }
    public DbSet<Depoimentos> Depoimentos { get; set; }
    public DbSet<Destinos> Destinos { get; set; }
}
