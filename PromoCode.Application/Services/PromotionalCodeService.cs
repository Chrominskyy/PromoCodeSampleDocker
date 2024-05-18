using Chrominsky.Utils.Mappers.Base;
using Chrominsky.Utils.Services;
using PromoCode.Domain.Enums;
using PromoCode.Domain.Models;
using PromoCode.Infrastructure.Repositories;

namespace PromoCode.Application.Services;

/// <inheritdoc />
public class PromotionalCodeService : IPromotionalCodeService
{
    private readonly IPromotionalCodeRepository _promotionalCodeRepository;
    private readonly ICacheService _cacheService;
    private readonly IBaseMapper<PromotionalCode, PromotionalCodeDto> _baseMapper;
    
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionalCodeService"/> class.
    /// </summary>
    /// <param name="promotionalCodeRepository">An instance of <see cref="IPromotionalCodeRepository"/> for managing promotional code data.</param>
    /// <param name="cacheService">An instance of <see cref="ICacheService"/> for caching promotional code data.</param>
    /// <param name="baseMapper">An instance of <see cref="IBaseMapper{PromotionalCode, PromotionalCodeDto}"/> for mapping promotional code data between domain and data transfer objects.</param>
    public PromotionalCodeService(
        IPromotionalCodeRepository promotionalCodeRepository,
        ICacheService cacheService,
        IBaseMapper<PromotionalCode, PromotionalCodeDto> baseMapper
    )
    {
        _promotionalCodeRepository = promotionalCodeRepository ?? throw new ArgumentNullException(nameof(promotionalCodeRepository));
        _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        _baseMapper = baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PromotionalCodeDto>> GetActivePromotionalCodes()
    {
        var ret = await _promotionalCodeRepository.GetPromotionalCodesAsync();
        var response = new List<PromotionalCodeDto>(ret.Select(x => _baseMapper.ToDto(x)));
        return response;
    }
    
    /// <inheritdoc />
    public async Task<PromotionalCodeDto?> GetPromotionalCode(Guid id)
    { 
        var ret = await _cacheService.GetOrAddAsync(id.ToString(),
            async () => await _promotionalCodeRepository.GetPromotionalCodeByIdAsync(id), null);
        
        return ret != null ? _baseMapper.ToDto(ret) : null;
    }

    /// <inheritdoc />
    public async Task<Guid> CreatePromotionalCode(PromotionalCodeDto promotionalCode)
    {
        return await _promotionalCodeRepository.AddPromotionalCodeAsync(_baseMapper.ToEntity(promotionalCode));
    }


    /// <inheritdoc />
    public async Task<PromotionalCodeDto> UpdatePromotionalCode(PromotionalCodeDto promotionalCode)
    {
        return _baseMapper.ToDto(
            await _promotionalCodeRepository.UpdatePromotionalCodeAsync(
                _baseMapper.ToEntity(promotionalCode)
            )
        );
    }

    /// <inheritdoc />
    public async Task<bool> DeletePromotionalCode(Guid id, string updatedBy)
    {
        await _promotionalCodeRepository.DeletePromotionalCodeAsync(id, updatedBy);
        await _cacheService.RemoveAsync(id.ToString());
        
        return true;
    }

    /// <inheritdoc />
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
            Id = codeObj.Id,
            UpdatedBy = "System - Redeemed"
        };
        
        // 3. Update PromotionalCode
        await _promotionalCodeRepository.UpdatePromotionalCodeAsync(newCode);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeactivatePromotionalCode(Guid id, string updatedBy)
    {
        var codeObj = await _promotionalCodeRepository.GetPromotionalCodeByIdAsync(id);
        if (codeObj == null) return false;
        await _promotionalCodeRepository.UpdatePromotionalCodeAsync(
            new PromotionalCode
            {
                Status = StatusEnum.Inactive,
                Id = codeObj.Id,
                UpdatedBy = updatedBy
            }
        );
        return true;
    }
}