using Taller.Domain.Common;

namespace Taller.Domain.Entities;

public sealed class MovimientoStock : BaseEntity
{
    public string Tipo { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public int StockItemId { get; set; }
    public StockItem? StockItem { get; set; }
}
