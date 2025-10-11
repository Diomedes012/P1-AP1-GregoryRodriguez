using Microsoft.EntityFrameworkCore;
using P1_AP1_GregoryRodriguez.Models;

namespace P1_AP1_GregoryRodriguez.Data;
public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
        
    }
    public DbSet<EntradasGuacales> EntradasGuacales { get; set; }
    public DbSet<TiposHuacales> TiposHuacales { get; set; }
    public DbSet<EntradasHuacalesDetalle> EntradasHuacalesDetalle { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TiposHuacales>().HasData(
            new List<TiposHuacales>
            {
                new()
                {
                    TipoId = 1,
                    Descripcion = "Verde",
                    Existencia = 0
                },
                new()
                {
                    TipoId = 2,
                    Descripcion = "Rojo",
                    Existencia = 0
                }
                
            }
        );
        base.OnModelCreating(modelBuilder);
    }
}

