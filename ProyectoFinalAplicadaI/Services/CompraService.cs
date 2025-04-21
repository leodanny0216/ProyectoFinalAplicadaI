using Microsoft.EntityFrameworkCore;
using ProyectoFinalAplicadaI.Data;
using ProyectoFinalAplicadaI.Models;
using System.Linq.Expressions;

namespace ProyectoFinalAplicadaI.Services;

public class CompraService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    public async Task<bool> Existe(int CompraId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Compras.AnyAsync(c => c.ComprasId == CompraId);
    }
    public async Task<Compras?> GetCompras(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Compras.Include(c => c.ComprasDetalle).FirstOrDefaultAsync(c => c.ComprasId == id);
    }
    public async Task<bool> Insertar(Compras Compras)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Compras.Add(Compras);
        int filasAfectadas = await contexto.SaveChangesAsync();
        return filasAfectadas > 0;
    }
    public async Task<bool> Modificar(Compras Compras)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var c = await contexto.Compras.FindAsync(Compras.ComprasId);
        contexto.Entry(c!).State = EntityState.Detached;
        contexto.Entry(Compras).State = EntityState.Modified;
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Guardar(Compras Compras)
    {
        if (!await Existe(Compras.ComprasId))
            return await Insertar(Compras);
        else
            return await Modificar(Compras);
    }
    public async Task<bool> Eliminar(int compra)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var compras = await contexto.Compras
                      .Include(c => c.ComprasDetalle)
                      .FirstOrDefaultAsync(c => c.ComprasId == compra);
        if (compra != null)
        {
            contexto.Compras.Remove(compras);
            await contexto.SaveChangesAsync();
            return true;
        }
        return false;
    }
    public async Task<Compras?> Buscar(int CompraId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Compras
            .Where(c => c.ComprasId == CompraId)
            .AsNoTracking()
            .SingleOrDefaultAsync();
    }
    public async Task<List<Compras>> Listar(Expression<Func<Compras, bool>> Criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Compras
                .Where(Criterio)
                .AsNoTracking()
                .ToListAsync();
    }
}