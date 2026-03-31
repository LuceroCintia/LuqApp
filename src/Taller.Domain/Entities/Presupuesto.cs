using Taller.Domain.Common;
using Taller.Domain.Enums;

namespace Taller.Domain.Entities;

public sealed class Presupuesto : BaseEntity
{
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public EstadoPresupuesto Estado { get; set; } = EstadoPresupuesto.Pendiente;
    public int Revision { get; set; } = 1;
    public decimal Total { get; set; }

    public int SiniestroId { get; set; }
    public Siniestro? Siniestro { get; set; }

    public ICollection<DetallePresupuesto> Detalles { get; set; } = new List<DetallePresupuesto>();
}
