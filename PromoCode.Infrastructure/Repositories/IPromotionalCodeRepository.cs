using PromoCode.Domain.Models;

namespace PromoCode.Infrastructure.Repositories;

public interface IPromotionalCodeRepository
{
    Task<IEnumerable<PromotionalCode>> GetPromotionalCodesAsync();
    Task<PromotionalCode?> GetPromotionalCodeByIdAsync(Guid id);
    Task<Guid> AddPromotionalCodeAsync(PromotionalCode? promotionalCode);
    Task<PromotionalCode> UpdatePromotionalCodeAsync(PromotionalCode? promotionalCode);
    Task DeletePromotionalCodeAsync(Guid id);
}