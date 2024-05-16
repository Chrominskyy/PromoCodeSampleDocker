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
    public async Task AddVersion(string objectType, Guid objectId, Guid objectTenant, string? beforeValue, string? afterValue,
        string updatedBy)
    {
        var objectVersioning = new ObjectVersioning(objectType, objectId, objectTenant, beforeValue, afterValue, updatedBy);
        await _objectVersioningRepository.AddAsync(objectVersioning);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ObjectVersioning>> GetVersions(string objectType, Guid objectTenant, Guid objectId)
    {
        return await _objectVersioningRepository.GetByObjectAsync(objectType, objectTenant, objectId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ObjectVersioning>> GetVersionsByObjectId(string objectType, Guid objectTenant, Guid objectId)
    {
        throw new NotImplementedException();
    }
}