using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chrominsky.Utils.Models.Base;

namespace SimpleTenant.Models;

/// <summary>
/// Represents a tenant in the application.
/// </summary>
[Table("Tenants")]
public class Tenant : BaseDatabaseEntity
{
    /// <summary>
    /// Unique identifier for the tenant.
    /// </summary>
    [Required]
    public required Guid TenantId { get; set; }
    
    /// <summary>
    /// Name of the tenant.
    /// </summary>
    [Required]
    public required string TenantName { get; set; }
    
    /// <summary>
    /// Indicates whether the tenant is currently active.
    /// </summary>
    [Required]
    public required bool IsActive { get; set; }
}