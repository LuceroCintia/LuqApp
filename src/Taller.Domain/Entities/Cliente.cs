using Taller.Domain.Common;

namespace Taller.Domain.Entities;

public sealed class Cliente : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public ICollection<Siniestro> Siniestros { get; set; } = new List<Siniestro>();
}
