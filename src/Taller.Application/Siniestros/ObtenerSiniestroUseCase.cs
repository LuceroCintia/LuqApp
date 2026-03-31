using Taller.Application.Abstractions;
using Taller.Application.DTOs;

namespace Taller.Application.Siniestros;

public sealed class ObtenerSiniestroUseCase
{
    private readonly ISiniestroRepository _repository;

    public ObtenerSiniestroUseCase(ISiniestroRepository repository)
    {
        _repository = repository;
    }

    public async Task<SiniestroDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var siniestro = await _repository.GetByIdAsync(id, cancellationToken);
        if (siniestro is null) return null;

        return new SiniestroDto
        {
            Id = siniestro.Id,
            FechaIngreso = siniestro.FechaIngreso,
            Descripcion = siniestro.Descripcion,
            Tipo = siniestro.Tipo.ToString(),
            NroSiniestro = siniestro.NroSiniestro,
            Poliza = siniestro.Poliza,
            ClienteId = siniestro.ClienteId,
            VehiculoId = siniestro.VehiculoId
        };
    }
}
