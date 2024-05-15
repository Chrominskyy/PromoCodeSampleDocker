using System.ComponentModel.DataAnnotations;

namespace PromoCode.Domain.Models.Base;

/// <summary>
/// Represents a base class for database entities.
/// </summary>
public class BaseDatabaseEntity
{
    /// <summary>
    /// The unique identifier of the tenant.
    /// </summary>
    [Required]
    public Guid TenantId { get; set; }
    
    /// <summary>
    /// The date and time when the entity was created.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// The date and time when the entity was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// The user who created the entity.
    /// </summary>
    [Required]
    public string CreatedBy { get; set; }
    
    /// <summary>
    /// The user who last updated the entity.
    /// </summary>
    public string? UpdatedBy { get; set; }
}