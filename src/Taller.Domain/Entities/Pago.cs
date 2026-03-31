using Taller.Domain.Common;

namespace Taller.Domain.Entities;

public sealed class Pago : BaseEntity
{
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public decimal Monto { get; set; }
    public string Metodo { get; set; } = string.Empty;

    public int FacturaId { get; set; }
    public Factura? Factura { get; set; }
}
