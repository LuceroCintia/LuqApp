using Taller.Domain.Common;
using Taller.Domain.Enums;

namespace Taller.Domain.Entities;

public sealed class Usuario : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public RolUsuario Rol { get; set; }
    public int? SectorId { get; set; }
}
