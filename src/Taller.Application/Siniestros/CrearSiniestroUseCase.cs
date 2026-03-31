using Taller.Application.Abstractions;
using Taller.Application.DTOs;
using Taller.Domain.Entities;
using Taller.Domain.Enums;

namespace Taller.Application.Siniestros;

public sealed class CrearSiniestroUseCase
{
    private readonly ISiniestroRepository _repository;
    private readonly IAuditoriaService _auditoria;

    public CrearSiniestroUseCase(ISiniestroRepository repository, IAuditoriaService auditoria)
    {
        _repository = repository;
        _auditoria = auditoria;
    }

    public async Task<SiniestroDto> ExecuteAsync(CrearSiniestroRequest request, int usuarioId, CancellationToken cancellationToken = default)
    {
        var tipo = Enum.TryParse<TipoSiniestro>(request.Tipo, true, out var parsed) ? parsed : TipoSiniestro.Particular;

        var siniestro = new Siniestro
        {
            FechaIngreso = request.FechaIngreso,
            Descripcion = request.Descripcion,
            Tipo = tipo,
            NroSiniestro = request.NroSiniestro,
            Poliza = request.Poliza,
            ClienteId = request.ClienteId,
            VehiculoId = request.VehiculoId
        };

        var creado = await _repository.AddAsync(siniestro, cancellationToken);
        await _auditoria.RegistrarAsync(nameof(Siniestro), creado.Id, "crear", null, creado, usuarioId, cancellationToken);

        return new SiniestroDto
        {
            Id = creado.Id,
            FechaIngreso = creado.FechaIngreso,
            Descripcion = creado.Descripcion,
            Tipo = creado.Tipo.ToString(),
            NroSiniestro = creado.NroSiniestro,
            Poliza = creado.Poliza,
            ClienteId = creado.ClienteId,
            VehiculoId = creado.VehiculoId
        };
    }
}
