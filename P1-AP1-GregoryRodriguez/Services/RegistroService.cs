using P1_AP1_GregoryRodriguez.Models;
using Microsoft.EntityFrameworkCore;
using P1_AP1_GregoryRodriguez.Data;
using System.Linq.Expressions;

namespace P1_AP1_GregoryRodriguez.Services;
public class RegistroService(IDbContextFactory<Contexto> factory)
{
    public async Task<List<EntradasGuacales>> Listar(Expression<Func<EntradasGuacales, bool>> criterio)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.Registro.Where(criterio).ToListAsync();
    }
}

