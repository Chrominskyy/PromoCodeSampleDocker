using Microsoft.EntityFrameworkCore;
using PromoCode.Domain.Models;
using PromoCode.Infrastructure.Repositories;

namespace PromoCode.Application.Services;

public class PromotionalCodeService : IPromotionalCodeService
{
    private readonly IPromotionalCodeRepository _promotionalCodeRepository;

    public PromotionalCodeService(IPromotionalCodeRepository promotionalCodeRepository)
    {
        _promotionalCodeRepository = promotionalCodeRepository;
    }

    public async Task<IEnumerable<PromotionalCode>> GetActivePromotionalCodes()
    {
        return await _promotionalCodeRepository.GetPromotionalCodesAsync();
    }

    public async Task<PromotionalCode?> GetPromotionalCode(Guid id)
    {
        return await _promotionalCodeRepository.GetPromotionalCodeByIdAsync(id);
    }

    public async Task<Guid> CreatePromotionalCode(PromotionalCode promotionalCode)
    {
        return await _promotionalCodeRepository.AddPromotionalCodeAsync(promotionalCode);
    }

    public async Task<PromotionalCode> UpdatePromotionalCode(int id, PromotionalCode promotionalCode)
    {
        return await _promotionalCodeRepository.UpdatePromotionalCodeAsync(promotionalCode);
    }

    public async Task DeletePromotionalCode(Guid id)
    {
        await _promotionalCodeRepository.DeletePromotionalCodeAsync(id);
    }

    public Task<PromotionalCode> RedeemPromotionalCode(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PromotionalCode> DeactivatePromotionalCode(Guid id)
    {
        throw new NotImplementedException();
    }
}