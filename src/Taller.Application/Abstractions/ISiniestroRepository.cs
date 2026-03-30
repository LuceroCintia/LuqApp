using Taller.Domain.Entities;

namespace Taller.Application.Abstractions;

public interface ISiniestroRepository
{
    Task<Siniestro> AddAsync(Siniestro siniestro, CancellationToken cancellationToken = default);
    Task<Siniestro?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
