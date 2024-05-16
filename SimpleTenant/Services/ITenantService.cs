using SimpleTenant.Models;

namespace SimpleTenant.Services;
/// <summary>
/// Interface for tenant related operations.
/// </summary>
public interface ITenantService
{
    /// <summary>
    /// Creates a new tenant.
    /// </summary>
    /// <param name="tenant">The tenant object to be created.</param>
    /// <returns>The newly created tenant object.</returns>
    Task<Guid> CreateTenant(Tenant tenant);

    /// <summary>
    /// Retrieves a tenant by its unique identifier.
    /// </summary>
    /// <param name="tenantId">The unique identifier of the tenant.</param>
    /// <returns>The tenant object with the specified identifier.</returns>
    Task<Tenant?> GetTenantById(Guid tenantId);
    
    /// <summary>
    /// Retrieves all tenants.
    /// </summary>
    /// <returns>An enumerable collection of tenant objects.</returns>
    Task<IEnumerable<Tenant>> GetAllTenants();

    /// <summary>
    /// Updates an existing tenant.
    /// </summary>
    /// <param name="tenant">The tenant object to be updated.</param>
    /// <returns>The updated tenant object.</returns>
    Task<Tenant> UpdateTenant(Tenant tenant);

    /// <summary>
    /// Deletes a tenant by its unique identifier.
    /// </summary>
    /// <param name="tenantId">The unique identifier of the tenant.</param>
    Task<bool> DeleteTenant(Guid tenantId);
}