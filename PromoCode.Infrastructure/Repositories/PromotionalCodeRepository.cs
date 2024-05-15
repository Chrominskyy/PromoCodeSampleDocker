using Microsoft.EntityFrameworkCore;
using PromoCode.Domain.Models;
using PromoCode.Infrastructure.Contexts;

namespace PromoCode.Infrastructure.Repositories;

public class PromotionalCodeRepository(PromoCodeDbContext context) : IPromotionalCodeRepository
{
    private readonly PromoCodeDbContext _context = context;

    public async Task<IEnumerable<PromotionalCode>> GetPromotionalCodesAsync()
    {
        if (_context.PromotionalCodes != null)
            return await _context.PromotionalCodes.ToListAsync();
        return new List<PromotionalCode>();
    }

    public async Task<PromotionalCode?> GetPromotionalCodeByIdAsync(Guid id)
    {
        if (_context.PromotionalCodes != null)
            return await _context.PromotionalCodes.FindAsync(id);
        return null;
    }

    public async Task<Guid> AddPromotionalCodeAsync(PromotionalCode? promotionalCode)
    {
        Guid id = Guid.NewGuid();
        if (promotionalCode != null)
        {
            promotionalCode.Id = id;
            await _context.PromotionalCodes.AddAsync(promotionalCode);
            await _context.SaveChangesAsync();
        }
        return id;
    }

    public async Task<PromotionalCode> UpdatePromotionalCodeAsync(PromotionalCode promotionalCode)
    {
        if (promotionalCode != null)
        {
            _context.PromotionalCodes.Update(promotionalCode);
            await _context.SaveChangesAsync();
        }

        return promotionalCode;
    }

    public async Task DeletePromotionalCodeAsync(Guid id)
    {
        if (_context.PromotionalCodes != null)
        {
            var promotionalCode = await _context.PromotionalCodes.FindAsync(id);
            if (promotionalCode != null)
            {
                _context.PromotionalCodes.Remove(promotionalCode);
                await _context.SaveChangesAsync();
            }
        }
    }
}