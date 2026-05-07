using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public sealed class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;

    public RefreshTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(RefreshToken entity)
    {
        await _context.RefreshToken.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public Task<RefreshToken?> GetByHashAsync(string tokenHash)
    {
        return _context.RefreshToken
            .FirstOrDefaultAsync(t => t.TokenHash == tokenHash);
    }

    public async Task UpdateAsync(RefreshToken entity)
    {
        entity.DateModified = DateTime.UtcNow;
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task RevokeFamilyAsync(Guid family, DateTime revokedAt)
    {
        var tokens = await _context.RefreshToken
            .Where(t => t.Family == family && t.RevokedAt == null)
            .ToListAsync();

        if (tokens.Count == 0) return;

        foreach (var token in tokens)
        {
            token.RevokedAt = revokedAt;
            token.DateModified = revokedAt;
        }

        await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteExpiredAsync(DateTime cutoff)
    {
        var expired = await _context.RefreshToken
            .Where(t => t.ExpiresAt < cutoff)
            .ToListAsync();

        if (expired.Count == 0) return 0;

        _context.RefreshToken.RemoveRange(expired);
        await _context.SaveChangesAsync();
        return expired.Count;
    }
}
