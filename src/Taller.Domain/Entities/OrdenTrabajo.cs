using Taller.Domain.Common;
using Taller.Domain.Enums;

namespace Taller.Domain.Entities;

public sealed class OrdenTrabajo : BaseEntity
{
    public DateTime FechaInicio { get; set; } = DateTime.UtcNow;
    public DateTime? FechaFinEstimada { get; set; }
    public EstadoOrden Estado { get; set; } = EstadoOrden.Creada;

    public int PresupuestoId { get; set; }
    public Presupuesto? Presupuesto { get; set; }

    public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
