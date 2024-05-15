using PromoCode.Domain.Models;

namespace PromoCode.Application.Services;

public interface IPromotionalCodeService
{
    Task<IEnumerable<PromotionalCode>> GetActivePromotionalCodes();
    Task<PromotionalCode?> GetPromotionalCode(Guid id);
    Task<Guid> CreatePromotionalCode(PromotionalCode promotionalCode);
    Task<PromotionalCode> UpdatePromotionalCode(int id, PromotionalCode promotionalCode);
    Task DeletePromotionalCode(Guid id);
    Task<PromotionalCode> RedeemPromotionalCode(Guid id);
    Task<PromotionalCode> DeactivatePromotionalCode(Guid id);
}