using Microsoft.EntityFrameworkCore;
using Taller.Application.Abstractions;
using Taller.Domain.Entities;
using Taller.Infrastructure.Persistence;

namespace Taller.Infrastructure.Repositories;

public sealed class SiniestroRepository : ISiniestroRepository
{
    private readonly TallerDbContext _db;

    public SiniestroRepository(TallerDbContext db)
    {
        _db = db;
    }

    public async Task<Siniestro> AddAsync(Siniestro siniestro, CancellationToken cancellationToken = default)
    {
        _db.Siniestros.Add(siniestro);
        await _db.SaveChangesAsync(cancellationToken);
        return siniestro;
    }

    public Task<Siniestro?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _db.Siniestros.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
}
