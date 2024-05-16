using System.Text.Json;
using Chrominsky.Utils.Mappers.Base;
using Chrominsky.Utils.Services;
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
    private readonly IBaseMapper<PromotionalCode, PromotionalCodeDto> _baseMapper;
    

    /// <summary>
    /// Initializes a new instance of the PromotionalCodeService class.
    /// </summary>
    /// <param name="promotionalCodeRepository">The repository for managing promotional codes.</param>
    /// <param name="cacheService">The service for caching promotional code data.</param>
    /// <param name="objectVersioningService"></param>
    public PromotionalCodeService(
        IPromotionalCodeRepository promotionalCodeRepository,
        ICacheService cacheService,
        IBaseMapper<PromotionalCode, PromotionalCodeDto> baseMapper
    )
    {
        _promotionalCodeRepository = promotionalCodeRepository;
        _cacheService = cacheService;
        _baseMapper = baseMapper;
    }

    /// <summary>
    /// Retrieves all active promotional codes.
    /// </summary>
    /// <returns>An asynchronous task that returns an IEnumerable of PromotionalCode.</returns>
    public async Task<IEnumerable<PromotionalCodeDto>> GetActivePromotionalCodes()
    {
        var ret = await _promotionalCodeRepository.GetPromotionalCodesAsync();
        return new List<PromotionalCodeDto>(ret.Select(x => _baseMapper.ToDto(x)));
    }

    /// <summary>
    /// Retrieves a promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code.</param>
    /// <returns>An asynchronous task that returns a nullable PromotionalCode.</returns>
    public async Task<PromotionalCodeDto?> GetPromotionalCode(Guid id)
    { 
        var ret = await _cacheService.GetOrAddAsync(id.ToString(),
            async () => await _promotionalCodeRepository.GetPromotionalCodeByIdAsync(id), null);
        
        return ret != null ? _baseMapper.ToDto(ret) : null;
    }

    /// <summary>
    /// Creates a new promotional code.
    /// </summary>
    /// <param name="promotionalCode">The promotional code to be created.</param>
    /// <returns>An asynchronous task that returns the unique identifier of the newly created promotional code.</returns>
    public async Task<Guid> CreatePromotionalCode(PromotionalCodeDto promotionalCode)
    {
        return await _promotionalCodeRepository.AddPromotionalCodeAsync(_baseMapper.ToEntity(promotionalCode));
    }

    /// <summary>
    /// Updates an existing promotional code.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to be updated.</param>
    /// <param name="promotionalCode">The updated promotional code.</param>
    /// <returns>An asynchronous task that returns the updated PromotionalCode.</returns>
    public async Task<PromotionalCode> UpdatePromotionalCode(PromotionalCodeDto promotionalCode)
    {
        return await _promotionalCodeRepository.UpdatePromotionalCodeAsync(_baseMapper.ToEntity(promotionalCode));
    }

    /// <summary>
    /// Deletes a promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to be deleted.</param>
    /// <returns>An asynchronous task.</returns>
    public async Task<bool> DeletePromotionalCode(Guid id, string updatedBy)
    {
        await _promotionalCodeRepository.DeletePromotionalCodeAsync(id, updatedBy);
        await _cacheService.RemoveAsync(id.ToString());
        
        return true;
    }

    /// <summary>
    /// Redeems a promotional code by its unique identifier.
    /// </summary>
    /// <param name="code"></param>
    /// <returns>An asynchronous task that returns the redeemed PromotionalCode.</returns>
    /// <exception cref="NotImplementedException">Thrown when the method is not implemented.</exception>
    public async Task<bool> RedeemPromotionalCode(string code)
    {
        // 1. Validate if the code is still available
        var codeObj = await _promotionalCodeRepository.GetPromotionalCodeByCodeAsync(code);
        if (codeObj is null || codeObj.RemainingUses < 1)
            return false;
        
        // 2. Decrease RemainingUses
        var newCode = new PromotionalCode()
        {
            RemainingUses = codeObj.RemainingUses - 1,
            Id = codeObj.Id
        };
        
        // 3. Update PromotionalCode
        await _promotionalCodeRepository.UpdatePromotionalCodeAsync(newCode);
        return true;

    }

    /// <inheritdoc />
    public async Task<bool> DeactivatePromotionalCode(Guid id, string updatedBy)
    {
        // 1. Validate if the code is still available
        var codeObj = await _promotionalCodeRepository.GetPromotionalCodeByIdAsync(id);
        if (codeObj == null) return false;
        await _promotionalCodeRepository.UpdatePromotionalCodeAsync(
            new PromotionalCode
            {
                Status = "INACTIVE",
                Id = codeObj.Id,
                UpdatedBy = updatedBy
            }
        );
        return true;

    }
}