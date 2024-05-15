using System.ComponentModel.DataAnnotations;

namespace PromoCode.Domain.Models;

/// <summary>
/// Represents a DTO for promotional codes.
/// </summary>
public class PromotionalCodeDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the promotional code.
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the promotional code.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the code of the promotional code.
    /// </summary>
    [Required]
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the remaining number of uses for the promotional code.
    /// </summary>
    public int RemainingUses { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of uses for the promotional code.
    /// </summary>
    public int MaxUses { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the promotional code is active.
    /// </summary>
    public bool IsActive { get; set; }
}