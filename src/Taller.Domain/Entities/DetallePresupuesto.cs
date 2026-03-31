using Taller.Domain.Common;

namespace Taller.Domain.Entities;

public sealed class DetallePresupuesto : BaseEntity
{
    public string Descripcion { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public decimal Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public int PresupuestoId { get; set; }
    public Presupuesto? Presupuesto { get; set; }
}
