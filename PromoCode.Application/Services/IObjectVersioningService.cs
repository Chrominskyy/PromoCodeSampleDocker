using PromoCode.Domain.Models;

namespace PromoCode.Application.Services;

/// <summary>
/// Interface for managing object versioning.
/// </summary>
public interface IObjectVersioningService
{
    /// <summary>
    /// Adds a new version of an object.
    /// </summary>
    /// <param name="objectType">The type of the object.</param>
    /// <param name="objectId">The unique identifier of the object.</param>
    /// <param name="objectTenant">The tenant identifier of the object.</param>
    /// <param name="beforeValue">The value of the object before the update.</param>
    /// <param name="afterValue">The value of the object after the update.</param>
    /// <param name="updatedBy">The user who performed the update.</param>
    Task AddVersion(string objectType, Guid objectId, Guid objectTenant, string? beforeValue, string? afterValue, string updatedBy);
    
    /// <summary>
    /// Retrieves all versions of an object for a specific tenant.
    /// </summary>
    /// <param name="objectType">The type of the object.</param>
    /// <param name="objectTenant">The tenant identifier of the object.</param>
    /// <param name="objectId">The unique identifier of the object.</param>
    /// <returns>An enumeration of <see cref="ObjectVersioning"/> representing the versions of the object.</returns>
    Task<IEnumerable<ObjectVersioning>> GetVersions(string objectType, Guid objectTenant, Guid objectId);
    
    /// <summary>
    /// Retrieves all versions of an object for a specific tenant and object id.
    /// </summary>
    /// <param name="objectType">The type of the object.</param>
    /// <param name="objectTenant">The tenant identifier of the object.</param>
    /// <param name="objectId">The unique identifier of the object.</param>
    /// <returns>An enumeration of <see cref="ObjectVersioning"/> representing the versions of the object.</returns>
    Task<IEnumerable<ObjectVersioning>> GetVersionsByObjectId(string objectType, Guid objectTenant, Guid objectId);
}