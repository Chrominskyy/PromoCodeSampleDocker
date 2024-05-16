using Chrominsky.Utils.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Chrominsky.Utils.Repositories.Base;

public abstract class BaseDatabaseRepository<T> : IBaseDatabaseRepository<T> where T : class
{
    private readonly DbContext _dbContext;

    public BaseDatabaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> GetByIdAsync<T>(Guid id) where T : class
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<Guid> AddAsync<T>(T entity) where T : BaseDatabaseEntity
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<T> UpdateAsync<T>(T entity) where T : BaseDatabaseEntity 
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync<T>(Guid id) where T : BaseDatabaseEntity 
    {
        T entity = await _dbContext.FindAsync<T>(id);
        if (entity == null)
            return false;
        entity.IsDeleted = true;
        var rowsAffected = await _dbContext.SaveChangesAsync();
        return rowsAffected > 0;
        
    }
}