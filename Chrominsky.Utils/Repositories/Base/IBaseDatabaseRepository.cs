using Chrominsky.Utils.Models.Base;

namespace Chrominsky.Utils.Repositories.Base;

public interface IBaseDatabaseRepository<T>
{
    Task<T> GetByIdAsync<T>(Guid id) where T : class;
    Task<Guid> AddAsync<T>(T entity) where T : BaseDatabaseEntity;
    Task<T> UpdateAsync<T>(T entity) where T : BaseDatabaseEntity;
    Task<bool> DeleteAsync<T>(Guid id) where T : BaseDatabaseEntity;
}