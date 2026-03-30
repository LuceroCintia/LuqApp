namespace Taller.Application.Abstractions;

public interface IAuditoriaService
{
    Task RegistrarAsync(string entidad, int entidadId, string accion, object? valorAnterior, object? valorNuevo, int usuarioId, CancellationToken cancellationToken = default);
}
