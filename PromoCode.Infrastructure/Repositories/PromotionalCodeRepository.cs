using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PromoCode.Domain.Enums;
using PromoCode.Domain.Models;
using PromoCode.Infrastructure.Contexts;

namespace PromoCode.Infrastructure.Repositories;

/// <inheritdoc />
public class PromotionalCodeRepository : IPromotionalCodeRepository
{
    private readonly PromoCodeDbContext _context;
    private readonly IObjectVersioningRepository _objectVersioningRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionalCodeRepository"/> class.
    /// </summary>
    /// <param name="context">The database context for interacting with the PromotionalCode entities.</param>
    /// <param name="objectVersioningRepository">The repository for managing object versioning.</param>
    public PromotionalCodeRepository(PromoCodeDbContext context, IObjectVersioningRepository objectVersioningRepository)
    {
        _context = context;
        _objectVersioningRepository = objectVersioningRepository;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PromotionalCode>> GetPromotionalCodesAsync()
    {
        if (_context.PromotionalCodes!= null)
            return await _context.PromotionalCodes
                .Where(
                    x => x.Status == StatusEnum.Active
                    && !x.IsDeleted
                )
                .OrderByDescending(x => x.UpdatedAt)
                .ToListAsync();
        return new List<PromotionalCode>();
    }
    
    /// <inheritdoc />
    public async Task<PromotionalCode?> GetPromotionalCodeByIdAsync(Guid id)
    {
        if (_context.PromotionalCodes!= null)
            return await _context.PromotionalCodes
                .FirstOrDefaultAsync(
                    x=> x.Id == id 
                        && x.Status == StatusEnum.Active
                        && !x.IsDeleted
                );
        return null;
    }

    /// <inheritdoc />
    public async Task<PromotionalCode?> GetPromotionalCodeByCodeAsync(string code)
    {
        if(_context.PromotionalCodes!= null)
            return await _context.PromotionalCodes
                .FirstOrDefaultAsync(
                    x => x.Code == code 
                        && x.Status == StatusEnum.Active
                        && !x.IsDeleted
                );
        return null;
    }

    /// <inheritdoc />
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
                    (Guid)promotionalCode.TenantId!,
                    null,
                    JsonSerializer.Serialize(promotionalCode),
                    promotionalCode.CreatedBy
                ));
        }
        
        return id;
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
    public async Task DeletePromotionalCodeAsync(Guid id, string updatedBy)
    {
        if (_context.PromotionalCodes!= null)
        {
            var promotionalCode = await _context.PromotionalCodes.FindAsync(id);
            if (promotionalCode!= null)
            {
                var beforeValue = JsonSerializer.Serialize(promotionalCode);
                promotionalCode.IsDeleted = true;
                promotionalCode.Status = StatusEnum.Deleted;
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

    /// <inheritdoc />
    public async Task<int?> CheckCodeAvailability(string code)
    {
        var codeObj = await GetPromotionalCodeByCodeAsync(code);
        return codeObj?.RemainingUses;
    }
}