using PromoCode.Domain.Models;
using PromoCode.Infrastructure.Repositories;

namespace PromoCode.Application.Services;

/// <summary>
/// This class is responsible for managing promotional codes.
/// It implements the IPromotionalCodeService interface and utilizes the IPromotionalCodeRepository and ICacheService.
/// </summary>
public class PromotionalCodeService : IPromotionalCodeService
{
    private readonly IPromotionalCodeRepository _promotionalCodeRepository;
    private readonly ICacheService _cacheService;

    /// <summary>
    /// Initializes a new instance of the PromotionalCodeService class.
    /// </summary>
    /// <param name="promotionalCodeRepository">The repository for managing promotional codes.</param>
    /// <param name="cacheService">The service for caching promotional code data.</param>
    public PromotionalCodeService(IPromotionalCodeRepository promotionalCodeRepository, ICacheService cacheService)
    {
        _promotionalCodeRepository = promotionalCodeRepository;
        _cacheService = cacheService;
    }

    /// <summary>
    /// Retrieves all active promotional codes.
    /// </summary>
    /// <returns>An asynchronous task that returns an IEnumerable of PromotionalCode.</returns>
    public async Task<IEnumerable<PromotionalCode>> GetActivePromotionalCodes()
    {
        return await _promotionalCodeRepository.GetPromotionalCodesAsync();
    }

    /// <summary>
    /// Retrieves a promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code.</param>
    /// <returns>An asynchronous task that returns a nullable PromotionalCode.</returns>
    public async Task<PromotionalCode?> GetPromotionalCode(Guid id)
    {
        return await _cacheService.GetOrAddAsync(id.ToString(),
            async () => await _promotionalCodeRepository.GetPromotionalCodeByIdAsync(id), null);
    }

    /// <summary>
    /// Creates a new promotional code.
    /// </summary>
    /// <param name="promotionalCode">The promotional code to be created.</param>
    /// <returns>An asynchronous task that returns the unique identifier of the newly created promotional code.</returns>
    public async Task<Guid> CreatePromotionalCode(PromotionalCode promotionalCode)
    {
        return await _promotionalCodeRepository.AddPromotionalCodeAsync(promotionalCode);
    }

    /// <summary>
    /// Updates an existing promotional code.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to be updated.</param>
    /// <param name="promotionalCode">The updated promotional code.</param>
    /// <returns>An asynchronous task that returns the updated PromotionalCode.</returns>
    public async Task<PromotionalCode> UpdatePromotionalCode(int id, PromotionalCode promotionalCode)
    {
        return await _promotionalCodeRepository.UpdatePromotionalCodeAsync(promotionalCode);
    }

    /// <summary>
    /// Deletes a promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to be deleted.</param>
    /// <returns>An asynchronous task.</returns>
    public async Task DeletePromotionalCode(Guid id)
    {
        await _promotionalCodeRepository.DeletePromotionalCodeAsync(id);
        await _cacheService.RemoveAsync(id.ToString());
    }

    /// <summary>
    /// Redeems a promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to be redeemed.</param>
    /// <returns>An asynchronous task that returns the redeemed PromotionalCode.</returns>
    /// <exception cref="NotImplementedException">Thrown when the method is not implemented.</exception>
    public Task<PromotionalCode> RedeemPromotionalCode(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deactivates a promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to be deactivated.</param>
    /// <returns>An asynchronous task that returns the deactivated PromotionalCode.</returns>
    /// <exception cref="NotImplementedException">Thrown when the method is not implemented.</exception>
    public Task<PromotionalCode> DeactivatePromotionalCode(Guid id)
    {
        throw new NotImplementedException();
    }
}