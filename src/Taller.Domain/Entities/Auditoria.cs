using Taller.Domain.Common;

namespace Taller.Domain.Entities;

public sealed class Auditoria : BaseEntity
{
    public string Entidad { get; set; } = string.Empty;
    public int EntidadId { get; set; }
    public string Accion { get; set; } = string.Empty;
    public string? ValorAnterior { get; set; }
    public string? ValorNuevo { get; set; }
    public int UsuarioId { get; set; }
}
