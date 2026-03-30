using Taller.Domain.Common;

namespace Taller.Domain.Entities;

public sealed class Factura : BaseEntity
{
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }
    public string Estado { get; set; } = "Emitida";

    public int OrdenTrabajoId { get; set; }
    public OrdenTrabajo? OrdenTrabajo { get; set; }

    public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
