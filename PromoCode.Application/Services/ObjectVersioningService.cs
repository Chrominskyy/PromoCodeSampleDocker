using PromoCode.Domain.Models;
using PromoCode.Infrastructure.Repositories;

namespace PromoCode.Application.Services;

/// <inheritdoc />
public class ObjectVersioningService : IObjectVersioningService
{
    private readonly IObjectVersioningRepository _objectVersioningRepository;
    
    public ObjectVersioningService(IObjectVersioningRepository objectVersioningRepository)
    {
        _objectVersioningRepository = objectVersioningRepository;
    }

    /// <inheritdoc />
    public async Task AddVersion(ObjectVersioning objectVersion)
    {
        await _objectVersioningRepository.AddAsync(objectVersion);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ObjectVersioning>> GetVersions(string objectType, Guid objectTenant, Guid objectId)
    {
        return await _objectVersioningRepository.GetByObjectAsync(objectType, objectTenant, objectId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ObjectVersioning>> GetVersionsByObject(string objectType, Guid objectTenant, Guid objectId)
    {
        return await _objectVersioningRepository.GetByObjectAsync(objectType, objectTenant, objectId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ObjectVersioning>> GetVersionsByObjectId(Guid objectId)
    {
        return await _objectVersioningRepository.GetByObjectIdAsync(objectId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ObjectVersioning>> GetVersions()
    {
        return await _objectVersioningRepository.GetAllAsync();
    }
}