using P1_AP1_GregoryRodriguez.Models;
using Microsoft.EntityFrameworkCore;
using P1_AP1_GregoryRodriguez.Data;
using System.Linq.Expressions;

namespace P1_AP1_GregoryRodriguez.Services;
public class EntradasGuacalesService(IDbContextFactory<Contexto> factory)
{
    private enum TipoOperacion
    {
        Suma = 1,
        Resta = 2
    }

    public async Task<bool> Guardar(EntradasGuacales huacales)
    {
        await using var contexto = await factory.CreateDbContextAsync();

        if(!await Existe(huacales.IdEntrada))
        {
            return await Insertar(huacales);
        }
        else
        {
            return await Modificar(huacales);
        }
    }

    private async Task AfectarExistencia(EntradasHuacalesDetalle[] detalles, TipoOperacion tipoOperacion)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        foreach (var item in detalles)
        {
            var tipoHuacal = await contexto.TiposHuacales.SingleAsync(t => t.TipoId == item.TipoId);

            if (tipoOperacion == TipoOperacion.Suma)
            {
                tipoHuacal.Existencia += item.Cantidad;
            }
            else
            {
                tipoHuacal.Existencia -= item.Cantidad;
            }
        }
        
        await contexto.SaveChangesAsync();
    }

    private async Task<bool> Insertar(EntradasGuacales huacal)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        contexto.EntradasGuacales.Add(huacal);
        await AfectarExistencia(huacal.EntradaHuacalDetalle.ToArray(), TipoOperacion.Suma);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(EntradasGuacales huacal)
    {
        await using var contexto = await factory.CreateDbContextAsync();

        var detallesViejos = await contexto.EntradasHuacalesDetalle
            .AsNoTracking()
            .Where(e => e.IdEntrada == huacal.IdEntrada)
            .ToArrayAsync();

        await AfectarExistencia(detallesViejos, TipoOperacion.Resta);

        await contexto.EntradasHuacalesDetalle
            .Where(d => d.IdEntrada == huacal.IdEntrada)
            .ExecuteDeleteAsync();

        contexto.EntradasGuacales.Attach(huacal);
        contexto.Entry(huacal).State = EntityState.Modified;

        foreach (var detalle in huacal.EntradaHuacalDetalle)
        {
            contexto.Entry(detalle).State = EntityState.Added;
        }

        await AfectarExistencia(huacal.EntradaHuacalDetalle.ToArray(), TipoOperacion.Suma);

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
        return await contexto.EntradasGuacales
            .Include(e => e.EntradaHuacalDetalle)
                .ThenInclude(d => d.TipoHuacal)
            .AsNoTracking().FirstOrDefaultAsync(e => e.IdEntrada == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        var huacal = await contexto.EntradasGuacales
            .Include(e => e.EntradaHuacalDetalle)
            .FirstOrDefaultAsync(e => e.IdEntrada == id);

        if (huacal == null) return false;

        await AfectarExistencia(huacal.EntradaHuacalDetalle.ToArray(), TipoOperacion.Resta);

        contexto.EntradasHuacalesDetalle.RemoveRange(huacal.EntradaHuacalDetalle);
        contexto.EntradasGuacales.Remove(huacal);

        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
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

