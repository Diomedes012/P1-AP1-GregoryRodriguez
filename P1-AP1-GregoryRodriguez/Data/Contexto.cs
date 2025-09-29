using Microsoft.EntityFrameworkCore;
using P1_AP1_GregoryRodriguez.Models;

namespace P1_AP1_GregoryRodriguez.Data;
public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
        
    }
    public DbSet<EntradasGuacales> Registro { get; set; }
}

