using Microsoft.EntityFrameworkCore;
using Taller.Domain.Entities;

namespace Taller.Infrastructure.Persistence;

public sealed class TallerDbContext : DbContext
{
    public TallerDbContext(DbContextOptions<TallerDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Vehiculo> Vehiculos => Set<Vehiculo>();
    public DbSet<Siniestro> Siniestros => Set<Siniestro>();
    public DbSet<Presupuesto> Presupuestos => Set<Presupuesto>();
    public DbSet<DetallePresupuesto> DetallesPresupuesto => Set<DetallePresupuesto>();
    public DbSet<OrdenTrabajo> OrdenesTrabajo => Set<OrdenTrabajo>();
    public DbSet<Tarea> Tareas => Set<Tarea>();
    public DbSet<Sector> Sectores => Set<Sector>();
    public DbSet<Operario> Operarios => Set<Operario>();
    public DbSet<Factura> Facturas => Set<Factura>();
    public DbSet<Pago> Pagos => Set<Pago>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Auditoria> Auditorias => Set<Auditoria>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehiculo>().HasIndex(v => v.Patente).IsUnique();
        modelBuilder.Entity<Siniestro>().HasIndex(s => s.NroSiniestro).IsUnique();

        modelBuilder.Entity<Tarea>()
            .HasOne(t => t.Sector)
            .WithMany(s => s.Tareas)
            .HasForeignKey(t => t.SectorId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}
