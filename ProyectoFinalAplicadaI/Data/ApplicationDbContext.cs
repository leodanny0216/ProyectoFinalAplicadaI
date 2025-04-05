using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalAplicadaI.Models;

namespace ProyectoFinalAplicadaI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Compras> Compras { get; set; }
    public DbSet<ComprasDetalle> ComprasDetalles { get; set; }
    public DbSet<Insumos> Insumos { get; set; }
    public DbSet<Reclamos> Reclamos { get; set; }
}