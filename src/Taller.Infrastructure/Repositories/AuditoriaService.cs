using System.Text.Json;
using Taller.Application.Abstractions;
using Taller.Domain.Entities;
using Taller.Infrastructure.Persistence;

namespace Taller.Infrastructure.Repositories;

public sealed class AuditoriaService : IAuditoriaService
{
    private readonly TallerDbContext _db;

    public AuditoriaService(TallerDbContext db)
    {
        _db = db;
    }

    public async Task RegistrarAsync(string entidad, int entidadId, string accion, object? valorAnterior, object? valorNuevo, int usuarioId, CancellationToken cancellationToken = default)
    {
        var item = new Auditoria
        {
            Entidad = entidad,
            EntidadId = entidadId,
            Accion = accion,
            ValorAnterior = valorAnterior is null ? null : JsonSerializer.Serialize(valorAnterior),
            ValorNuevo = valorNuevo is null ? null : JsonSerializer.Serialize(valorNuevo),
            UsuarioId = usuarioId
        };

        _db.Auditorias.Add(item);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
