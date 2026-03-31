using Taller.Domain.Common;

namespace Taller.Domain.Entities;

public sealed class Operario : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public int SectorId { get; set; }
    public Sector? Sector { get; set; }
}
