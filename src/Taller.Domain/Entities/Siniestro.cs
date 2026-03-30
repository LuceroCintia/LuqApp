using Taller.Domain.Common;
using Taller.Domain.Enums;

namespace Taller.Domain.Entities;

public sealed class Siniestro : BaseEntity
{
    public DateTime FechaIngreso { get; set; } = DateTime.UtcNow;
    public string Descripcion { get; set; } = string.Empty;
    public TipoSiniestro Tipo { get; set; }
    public string NroSiniestro { get; set; } = string.Empty;
    public string? Poliza { get; set; }

    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    public int VehiculoId { get; set; }
    public Vehiculo? Vehiculo { get; set; }

    public ICollection<Presupuesto> Presupuestos { get; set; } = new List<Presupuesto>();
}
