using Taller.Domain.Common;
using Taller.Domain.Enums;

namespace Taller.Domain.Entities;

public sealed class Tarea : BaseEntity
{
    public string Descripcion { get; set; } = string.Empty;
    public EstadoTarea Estado { get; set; } = EstadoTarea.Pendiente;

    public int OrdenTrabajoId { get; set; }
    public OrdenTrabajo? OrdenTrabajo { get; set; }

    public int SectorId { get; set; }
    public Sector? Sector { get; set; }
}
