using Chrominsky.Utils.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using PromoCode.Domain.Models;
using PromoCode.Infrastructure.Contexts;

namespace PromoCode.Infrastructure.Repositories;

/// <inheritdoc />
public class ObjectVersioningRepository : IObjectVersioningRepository
{
    private readonly PromoCodeDbContext _dbContext;
    
    public ObjectVersioningRepository(PromoCodeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<ObjectVersioning?> GetByIdAsync(Guid id)
    {
        return await _dbContext.FindAsync<ObjectVersioning>(id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ObjectVersioning>> GetByObjectAsync(string objectType, Guid objectTenant, Guid objectId)
    {
        if (_dbContext.ObjectVersionings != null)
            return await _dbContext.ObjectVersionings
                .Where(
                    x => x.ObjectType == objectType
                         && x.ObjectTenant == objectTenant
                         && x.ObjectId == objectId
                )
                .OrderByDescending(x => x.UpdatedOn)
                .ToListAsync();
        return new List<ObjectVersioning>();
    }

    /// <inheritdoc />
    public async Task<Guid> AddAsync(ObjectVersioning entity)
    {
        await _dbContext.ObjectVersionings.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<ObjectVersioning> UpdateAsync(ObjectVersioning entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ObjectVersioning>> GetAllAsync()
    {
        return await _dbContext.ObjectVersionings
            .OrderByDescending(x => x.UpdatedOn)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ObjectVersioning>> GetByObjectIdAsync(Guid objectId)
    {
        return await _dbContext.ObjectVersionings
            .Where(x => x.ObjectId == objectId)
            .OrderByDescending(x => x.UpdatedOn)
            .ToListAsync();
    }
}