using Taller.Domain.Common;

namespace Taller.Domain.Entities;

public sealed class StockItem : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public int StockMinimo { get; set; }
    public ICollection<MovimientoStock> Movimientos { get; set; } = new List<MovimientoStock>();
}
