using Microsoft.EntityFrameworkCore;
using ProyectoFinalAplicadaI.Data;
using ProyectoFinalAplicadaI.Models;
using System.Linq.Expressions;

namespace ProyectoFinalAplicadaI.Services;

public class ReclamoService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    public async Task<bool> Existe(int ReclamoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Reclamos.AnyAsync(r => r.ReclamoId == ReclamoId);
    }
    public async Task<bool> Insertar(Reclamos reclamo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Reclamos.Add(reclamo);
        int filasAfectadas = await contexto.SaveChangesAsync();
        return filasAfectadas > 0;
    }
    public async Task<bool> Modificar(Reclamos reclamo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var r = await contexto.Reclamos.FindAsync(reclamo.ReclamoId);
        contexto.Entry(r!).State = EntityState.Detached;
        contexto.Entry(reclamo).State = EntityState.Modified;
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Guardar(Reclamos reclamo)
    {
        if (!await Existe(reclamo.ReclamoId))
            return await Insertar(reclamo);
        else
            return await Modificar(reclamo);
    }
    public async Task<bool> Eliminar(int reclamo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var eliminarReclamos  = await contexto.Reclamos 
             .Where(c => c.ReclamoId == reclamo)
             .ExecuteDeleteAsync();
        return eliminarReclamos > 0;
    }
    public async Task<Reclamos?> Buscar(int ReclamoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Reclamos
            .Where(s => s.ReclamoId == ReclamoId)
            .AsNoTracking()
            .SingleOrDefaultAsync();
    }
    public async Task<List<Reclamos>> Listar(Expression<Func<Reclamos, bool>> Criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Reclamos
                .Where(Criterio)
                .AsNoTracking()
                .ToListAsync();
    }
}