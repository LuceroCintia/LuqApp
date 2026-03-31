using Taller.Domain.Common;

namespace Taller.Domain.Entities;

public sealed class Vehiculo : BaseEntity
{
    public string Patente { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Anio { get; set; }
    public ICollection<Siniestro> Siniestros { get; set; } = new List<Siniestro>();
}
