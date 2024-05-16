using PromoCode.Domain.Models;

namespace PromoCode.Application.Services;

/// <summary>
/// Interface for managing object versioning.
/// </summary>
public interface IObjectVersioningService
{
    
    /// <summary>
    /// Adds a new version of an object to the versioning system.
    /// </summary>
    /// <param name="objectVersion">The <see cref="ObjectVersioning"/> object representing the new version.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddVersion(ObjectVersioning objectVersion);
    
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
    Task<IEnumerable<ObjectVersioning>> GetVersionsByObject(string objectType, Guid objectTenant, Guid objectId);
    
    /// <summary>
    /// Retrieves all versions of an object by its unique identifier.
    /// </summary>
    /// <param name="objectId">The unique identifier of the object.</param>
    /// <returns>An enumeration of <see cref="ObjectVersioning"/> representing the versions of the object.</returns>
    Task<IEnumerable<ObjectVersioning>> GetVersionsByObjectId(Guid objectId);
    
    /// <summary>
    /// Retrieves all versions of objects.
    /// </summary>
    /// <returns>An enumeration of <see cref="ObjectVersioning"/> representing the versions of all objects.</returns>
    Task<IEnumerable<ObjectVersioning>> GetVersions();
}