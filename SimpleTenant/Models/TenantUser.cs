using System.ComponentModel.DataAnnotations.Schema;
using Chrominsky.Utils.Models.Base;

namespace SimpleTenant.Models;

/// <summary>
/// Represents a user's association with a tenant in the application.
/// </summary>
[Table("TenantUsers")]
public class TenantUser : BaseDatabaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the tenant.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is currently active in the tenant.
    /// </summary>
    public bool IsActive { get; set; }
}