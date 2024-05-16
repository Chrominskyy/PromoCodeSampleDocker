using System.Text.Json;
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
    private readonly IObjectVersioningRepository _objectVersioningRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionalCodeRepository"/> class.
    /// </summary>
    /// <param name="context">The database context for interacting with the PromotionalCode entities.</param>
    public PromotionalCodeRepository(PromoCodeDbContext context, IObjectVersioningRepository objectVersioningRepository)
    {
        _context = context;
        _objectVersioningRepository = objectVersioningRepository;
    }

    /// <summary>
    /// Retrieves all PromotionalCode entities from the database.
    /// </summary>
    /// <returns>An asynchronous task that returns an IEnumerable of PromotionalCode entities.</returns>
    public async Task<IEnumerable<PromotionalCode>> GetPromotionalCodesAsync()
    {
        if (_context.PromotionalCodes!= null)
            return await _context.PromotionalCodes
                .Where(
                    x => x.Status == "ACTIVE" 
                    && !x.IsDeleted
                )
                .ToListAsync();
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
            return await _context.PromotionalCodes
                .FirstOrDefaultAsync(
                    x=> x.Id == id 
                        && x.Status == "ACTIVE"
                        && !x.IsDeleted
                );
        return null;
    }

    /// <summary>
    /// Retrieves a PromotionalCode entity by its unique code from the database.
    /// The method only returns active PromotionalCode entities.
    /// </summary>
    /// <param name="code">The unique code of the PromotionalCode entity.</param>
    /// <returns>
    /// An asynchronous task that returns the PromotionalCode entity with the specified code and status "ACTIVE", 
    /// or null if not found.
    /// </returns>
    public async Task<PromotionalCode?> GetPromotionalCodeByCodeAsync(string code)
    {
        if(_context.PromotionalCodes!= null)
            return await _context.PromotionalCodes
                .FirstOrDefaultAsync(
                    x => x.Status == code 
                        && x.Status == "ACTIVE"
                );
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
            promotionalCode.CreatedAt = DateTime.UtcNow;
            await _context.PromotionalCodes.AddAsync(promotionalCode);
            await _context.SaveChangesAsync();
            await _objectVersioningRepository.AddAsync(
                new ObjectVersioning(
                    nameof(promotionalCode),
                    id,
                    (Guid)promotionalCode.TenantId,
                    null,
                    JsonSerializer.Serialize(promotionalCode),
                    promotionalCode.CreatedBy
                ));
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
        if (promotionalCode == null)
        {
            throw new ArgumentNullException(nameof(promotionalCode));
        }

        var existingCode = await _context.PromotionalCodes.FindAsync(promotionalCode.Id);
        if (existingCode == null)
        {
            throw new KeyNotFoundException("Promotional code not found.");
        }

        promotionalCode.UpdatedAt = DateTime.UtcNow;
        var objectVersioning = new ObjectVersioning()
        {
            ObjectType = nameof(promotionalCode),
            ObjectId = promotionalCode.Id,
            BeforeValue = JsonSerializer.Serialize(existingCode),
            UpdatedOn = DateTime.UtcNow,
            UpdatedBy = promotionalCode.UpdatedBy
        };
        objectVersioning.ObjectTenant = (Guid)(promotionalCode.TenantId ?? existingCode.TenantId)!;

        var properties = typeof(PromotionalCode).GetProperties();
        foreach (var property in properties)
        {
            var newValue = property.GetValue(promotionalCode);
            if (newValue != null)
            {
                if(newValue is DateTime && (DateTime)newValue == DateTime.MinValue)
                    continue;
                if(newValue is Guid && (Guid)newValue == Guid.Empty)
                    continue;
                
                property.SetValue(existingCode, newValue);
            }
        }

        await _context.SaveChangesAsync();

        objectVersioning.AfterValue = JsonSerializer.Serialize(existingCode);
        await _objectVersioningRepository.AddAsync(objectVersioning);

        return existingCode;
    }

    /// <summary>
    /// Deletes a PromotionalCode entity from the database by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the PromotionalCode entity to be deleted.</param>
    /// <returns>An asynchronous task that completes the deletion operation.</returns>
    public async Task DeletePromotionalCodeAsync(Guid id, string updatedBy)
    {
        if (_context.PromotionalCodes!= null)
        {
            var promotionalCode = await _context.PromotionalCodes.FindAsync(id);
            if (promotionalCode!= null)
            {
                var beforeValue = JsonSerializer.Serialize(promotionalCode);
                promotionalCode.IsDeleted = true;
                promotionalCode.Status = "DELETED";
                _context.PromotionalCodes.Update(promotionalCode);
                await _context.SaveChangesAsync();
                
                await _objectVersioningRepository.AddAsync(
                    new ObjectVersioning(
                        nameof(promotionalCode),
                        promotionalCode.Id,
                        (Guid)promotionalCode.TenantId!,
                        beforeValue,
                        JsonSerializer.Serialize(promotionalCode),
                        updatedBy
                    )
                );
            }
        }
    }

    /// <summary>
    /// Checks the availability of a promotional code in the database.
    /// </summary>
    /// <param name="code">The unique code of the promotional code to check.</param>
    /// <returns>
    /// An asynchronous task that returns true if the code is found and has remaining uses, 
    /// otherwise returns false.
    /// </returns>
    public async Task<int?> CheckCodeAvailability(string code)
    {
        var codeObj = await GetPromotionalCodeByCodeAsync(code);
        return codeObj?.RemainingUses;
    }
}