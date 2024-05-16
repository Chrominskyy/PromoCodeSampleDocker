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

    /// <summary>
    /// Retrieves a promotional code by its unique code.
    /// </summary>
    /// <param name="code">The unique code of the promotional code.</param>
    /// <returns>
    /// An asynchronous task that returns a nullable PromotionalCode.
    /// If the code does not exist in the repository, the method will return null.
    /// </returns>
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
    /// Deletes a promotional code from the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to be deleted.</param>
    /// <param name="updatedBy">The identifier of the user or process that initiated the deletion.</param>
    /// <returns>
    /// An asynchronous task that does not return any value.
    /// If the promotional code with the specified id does not exist in the repository, the method will not throw an exception.
    /// </returns>
    Task DeletePromotionalCodeAsync(Guid id, string updatedBy);

    /// <summary>
    /// Checks the availability of a promotional code in the repository.
    /// </summary>
    /// <param name="code">The unique code of the promotional code to be checked.</param>
    /// <returns>
    /// An asynchronous task that returns a nullable integer.
    /// If the code exists in the repository, the method will return the count of occurrences.
    /// If the code does not exist in the repository, the method will return null.
    /// </returns>
    Task<int?> CheckCodeAvailability(string code);
}