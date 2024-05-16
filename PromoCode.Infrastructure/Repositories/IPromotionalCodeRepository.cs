using PromoCode.Domain.Models;

namespace PromoCode.Infrastructure.Repositories;

/// <summary>
/// Interface for interacting with the Promotional Code repository.
/// </summary>
public interface IPromotionalCodeRepository
{
    /// <summary>
    /// Retrieves all promotional codes from the repository.
    /// </summary>
    /// <returns>An asynchronous task that returns an IEnumerable of PromotionalCode.</returns>
    Task<IEnumerable<PromotionalCode>> GetPromotionalCodesAsync();

    /// <summary>
    /// Retrieves a promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code.</param>
    /// <returns>An asynchronous task that returns a nullable PromotionalCode.</returns>
    Task<PromotionalCode?> GetPromotionalCodeByIdAsync(Guid id);

    Task<PromotionalCode?> GetPromotionalCodeByCodeAsync(string code);

    /// <summary>
    /// Adds a new promotional code to the repository.
    /// </summary>
    /// <param name="promotionalCode">The promotional code to be added.</param>
    /// <returns>An asynchronous task that returns the unique identifier of the added promotional code.</returns>
    Task<Guid> AddPromotionalCodeAsync(PromotionalCode? promotionalCode);

    /// <summary>
    /// Updates an existing promotional code in the repository.
    /// </summary>
    /// <param name="promotionalCode">The updated promotional code.</param>
    /// <returns>An asynchronous task that returns the updated PromotionalCode.</returns>
    Task<PromotionalCode> UpdatePromotionalCodeAsync(PromotionalCode? promotionalCode);

    /// <summary>
    /// Deletes a promotional code from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to be deleted.</param>
    /// <returns>An asynchronous task.</returns>
    Task DeletePromotionalCodeAsync(Guid id);

    Task<int?> CheckCodeAvailability(string code);
}