using Microsoft.EntityFrameworkCore;
using ProyectoFinalAplicadaI.Data;
using ProyectoFinalAplicadaI.Models;
using System.Linq.Expressions;

namespace ProyectoFinalAplicadaI.Services;

public class ClienteService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    //Metodo existe
    public async Task<bool> Existe(int clienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AnyAsync(c => c.ClienteId == clienteId);
    }
    public async Task<bool> ExisteNombreCliente(string cedula, int clienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        // Verifica si existe un cliente con la misma cédula y un ClienteId distinto al del cliente actual.
        return await contexto.Clientes
            .AnyAsync(c => c.Cedula.ToLower() == cedula.ToLower() && c.ClienteId != clienteId);
    }
    //Metodo Insertar
    private async Task<bool> Insertar(Clientes cliente)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Clientes.Add(cliente);
        return await contexto.SaveChangesAsync() > 0;
    }
    //Metodo Modificar
    public async Task<bool> Modificar(Clientes cliente)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(cliente);
        return await contexto.SaveChangesAsync() > 0;
    }
    //Metodo Guardar
    public async Task<bool> Guardar(Clientes cliente)
    {
        if (!await Existe(cliente.ClienteId))
            return await Insertar(cliente);
        else
            return await Modificar(cliente);
    }
    //Metodo Eliminar
    public async Task<bool> Eliminar(int cliente)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var eliminarCliente = await contexto.Clientes
             .Where(c => c.ClienteId == cliente)
             .ExecuteDeleteAsync();
        return eliminarCliente > 0;
    }
    //Metodo Buscar 
    public async Task<Clientes?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ClienteId == id);
    }
    //Metodo Listar
    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}