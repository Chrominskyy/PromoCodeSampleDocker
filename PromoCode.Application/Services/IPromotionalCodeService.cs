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
    Task<IEnumerable<PromotionalCode>> GetActivePromotionalCodes();

    /// <summary>
    /// Gets a promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code.</param>
    /// <returns>The promotional code if found, otherwise null.</returns>
    Task<PromotionalCode?> GetPromotionalCode(Guid id);

    /// <summary>
    /// Creates a new promotional code.
    /// </summary>
    /// <param name="promotionalCode">The promotional code to create.</param>
    /// <returns>The unique identifier of the newly created promotional code.</returns>
    Task<Guid> CreatePromotionalCode(PromotionalCode promotionalCode);

    /// <summary>
    /// Updates an existing promotional code.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to update.</param>
    /// <param name="promotionalCode">The updated promotional code.</param>
    /// <returns>The updated promotional code.</returns>
    Task<PromotionalCode> UpdatePromotionalCode(int id, PromotionalCode promotionalCode);

    /// <summary>
    /// Deletes a promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to delete.</param>
    Task DeletePromotionalCode(Guid id);

    /// <summary>
    /// Redeems a promotional code.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to redeem.</param>
    /// <returns>The redeemed promotional code.</returns>
    Task<PromotionalCode> RedeemPromotionalCode(Guid id);

    /// <summary>
    /// Deactivates a promotional code.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to deactivate.</param>
    /// <returns>The deactivated promotional code.</returns>
    Task<PromotionalCode> DeactivatePromotionalCode(Guid id);
}