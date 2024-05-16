using PromoCode.Domain.Models;

namespace PromoCode.Application.Services;

/// <summary>
/// Interface for PromotionalCodeService.
/// </summary>
public interface IPromotionalCodeService
{
    /// <summary>
    /// Gets all active promotional codes.
    /// </summary>
    /// <returns>An enumeration of active promotional codes.</returns>
    Task<IEnumerable<PromotionalCodeDto>> GetActivePromotionalCodes();

    /// <summary>
    /// Gets a promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code.</param>
    /// <returns>The promotional code if found, otherwise null.</returns>
    Task<PromotionalCodeDto?> GetPromotionalCode(Guid id);

    /// <summary>
    /// Creates a new promotional code.
    /// </summary>
    /// <param name="promotionalCode">The promotional code to create.</param>
    /// <returns>The unique identifier of the newly created promotional code.</returns>
    Task<Guid> CreatePromotionalCode(PromotionalCodeDto promotionalCode);

    /// <summary>
    /// Updates an existing promotional code.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to update.</param>
    /// <param name="promotionalCode">The updated promotional code.</param>
    /// <returns>The updated promotional code.</returns>
    Task<PromotionalCode> UpdatePromotionalCode(PromotionalCodeDto promotionalCode);

    /// <summary>
    /// Deletes a promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to delete.</param>
    Task<bool> DeletePromotionalCode(Guid id, string updatedBy);

    /// <summary>
    /// Redeems a promotional code.
    /// </summary>
    /// <param name="code"></param>
    /// <returns>The redeemed promotional code.</returns>
    Task<bool> RedeemPromotionalCode(string code);
    
    Task<bool> DeactivatePromotionalCode(Guid id, string updatedBy);
}