using Taller.Domain.Common;

namespace Taller.Domain.Entities;

public sealed class Sector : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
