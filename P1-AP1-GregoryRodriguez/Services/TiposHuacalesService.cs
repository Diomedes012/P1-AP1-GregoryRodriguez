using P1_AP1_GregoryRodriguez.Models;
using Microsoft.EntityFrameworkCore;
using P1_AP1_GregoryRodriguez.Data;
using System.Linq.Expressions;
namespace P1_AP1_GregoryRodriguez.Services;

public class TiposHuacalesService(IDbContextFactory<Contexto> factory)
{
    public async Task<TiposHuacales> Buscar(int tipoId)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.TiposHuacales
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<List<TiposHuacales>> Listar(Expression<Func<TiposHuacales, bool>> criterio)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.TiposHuacales
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
