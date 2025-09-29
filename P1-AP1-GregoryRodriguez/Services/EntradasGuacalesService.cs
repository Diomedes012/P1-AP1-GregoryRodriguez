using P1_AP1_GregoryRodriguez.Models;
using Microsoft.EntityFrameworkCore;
using P1_AP1_GregoryRodriguez.Data;
using System.Linq.Expressions;

namespace P1_AP1_GregoryRodriguez.Services;
public class EntradasGuacalesService(IDbContextFactory<Contexto> factory)
{
    public async Task<bool> Guardar(EntradasGuacales guacales)
    {
        await using var contexto = await factory.CreateDbContextAsync();

        if(!await Existe(guacales.IdEntrada))
        {
            contexto.EntradasGuacales.Add(guacales);
        }
        else
        {
            contexto.EntradasGuacales.Update(guacales);
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.EntradasGuacales.AnyAsync(e => e.IdEntrada == id);
    }
    
    public async Task<EntradasGuacales?> Buscar(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.EntradasGuacales.FirstOrDefaultAsync(e => e.IdEntrada == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.EntradasGuacales
            .AsNoTracking()
            .Where(e => e.IdEntrada == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<EntradasGuacales>> Listar(Expression<Func<EntradasGuacales, bool>> criterio)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.EntradasGuacales
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}

