using Microsoft.EntityFrameworkCore;
using PromoCode.Domain.Models;
using PromoCode.Infrastructure.Contexts;

namespace PromoCode.Infrastructure.Repositories;

/// <summary>
/// This class is responsible for interacting with the database to perform CRUD operations on PromotionalCode entities.
/// </summary>
public class PromotionalCodeRepository : IPromotionalCodeRepository
{
    private readonly PromoCodeDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionalCodeRepository"/> class.
    /// </summary>
    /// <param name="context">The database context for interacting with the PromotionalCode entities.</param>
    public PromotionalCodeRepository(PromoCodeDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all PromotionalCode entities from the database.
    /// </summary>
    /// <returns>An asynchronous task that returns an IEnumerable of PromotionalCode entities.</returns>
    public async Task<IEnumerable<PromotionalCode>> GetPromotionalCodesAsync()
    {
        if (_context.PromotionalCodes!= null)
            return await _context.PromotionalCodes.ToListAsync();
        return new List<PromotionalCode>();
    }

    /// <summary>
    /// Retrieves a PromotionalCode entity by its unique identifier from the database.
    /// </summary>
    /// <param name="id">The unique identifier of the PromotionalCode entity.</param>
    /// <returns>An asynchronous task that returns the PromotionalCode entity with the specified identifier, or null if not found.</returns>
    public async Task<PromotionalCode?> GetPromotionalCodeByIdAsync(Guid id)
    {
        if (_context.PromotionalCodes!= null)
            return await _context.PromotionalCodes.FindAsync(id);
        return null;
    }

    /// <summary>
    /// Adds a new PromotionalCode entity to the database.
    /// </summary>
    /// <param name="promotionalCode">The PromotionalCode entity to be added.</param>
    /// <returns>An asynchronous task that returns the unique identifier of the newly added PromotionalCode entity.</returns>
    public async Task<Guid> AddPromotionalCodeAsync(PromotionalCode? promotionalCode)
    {
        Guid id = Guid.NewGuid();
        if (promotionalCode!= null)
        {
            promotionalCode.Id = id;
            await _context.PromotionalCodes.AddAsync(promotionalCode);
            await _context.SaveChangesAsync();
        }
        return id;
    }

    /// <summary>
    /// Updates an existing PromotionalCode entity in the database.
    /// </summary>
    /// <param name="promotionalCode">The PromotionalCode entity to be updated.</param>
    /// <returns>An asynchronous task that returns the updated PromotionalCode entity.</returns>
    public async Task<PromotionalCode> UpdatePromotionalCodeAsync(PromotionalCode promotionalCode)
    {
        if (promotionalCode!= null)
        {
            _context.PromotionalCodes.Update(promotionalCode);
            await _context.SaveChangesAsync();
        }

        return promotionalCode;
    }

    /// <summary>
    /// Deletes a PromotionalCode entity from the database by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the PromotionalCode entity to be deleted.</param>
    /// <returns>An asynchronous task that completes the deletion operation.</returns>
    public async Task DeletePromotionalCodeAsync(Guid id)
    {
        if (_context.PromotionalCodes!= null)
        {
            var promotionalCode = await _context.PromotionalCodes.FindAsync(id);
            if (promotionalCode!= null)
            {
                _context.PromotionalCodes.Remove(promotionalCode);
                await _context.SaveChangesAsync();
            }
        }
    }
}