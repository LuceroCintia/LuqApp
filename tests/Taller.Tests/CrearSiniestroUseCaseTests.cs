using Xunit;
using Taller.Application.Abstractions;
using Taller.Application.DTOs;
using Taller.Application.Siniestros;
using Taller.Domain.Entities;

public class CrearSiniestroUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_DeberiaCrearYAuditar()
    {
        var repo = new FakeRepo();
        var auditoria = new FakeAuditoria();
        var useCase = new CrearSiniestroUseCase(repo, auditoria);

        var request = new CrearSiniestroRequest
        {
            Descripcion = "Golpe lateral",
            Tipo = "Seguro",
            NroSiniestro = "SIN-001",
            ClienteId = 10,
            VehiculoId = 20
        };

        var dto = await useCase.ExecuteAsync(request, 99);

        Assert.Equal(1, dto.Id);
        Assert.Equal("Seguro", dto.Tipo);
        Assert.Equal("SIN-001", dto.NroSiniestro);
        Assert.True(auditoria.Registrado);
    }

    private sealed class FakeRepo : ISiniestroRepository
    {
        public Task<Siniestro> AddAsync(Siniestro siniestro, CancellationToken cancellationToken = default)
        {
            siniestro.Id = 1;
            return Task.FromResult(siniestro);
        }

        public Task<Siniestro?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => Task.FromResult<Siniestro?>(null);
    }

    private sealed class FakeAuditoria : IAuditoriaService
    {
        public bool Registrado { get; private set; }

        public Task RegistrarAsync(string entidad, int entidadId, string accion, object? valorAnterior, object? valorNuevo, int usuarioId, CancellationToken cancellationToken = default)
        {
            Registrado = entidad == nameof(Siniestro) && accion == "crear" && usuarioId == 99;
            return Task.CompletedTask;
        }
    }
}
