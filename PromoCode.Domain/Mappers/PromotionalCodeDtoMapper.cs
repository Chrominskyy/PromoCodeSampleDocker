using Chrominsky.Utils.Mappers.Base;
using PromoCode.Domain.Enums;
using PromoCode.Domain.Models;

namespace PromoCode.Domain.Mappers;

/// <inheritdoc />
public class PromotionalCodeDtoMapper : IBaseMapper<PromotionalCode, PromotionalCodeDto>
{
    /// <inheritdoc />
    public PromotionalCodeDto ToDto(PromotionalCode entity)
    {
        return new PromotionalCodeDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            Code = entity.Code,
            RemainingUses = entity.RemainingUses,
            MaxUses = entity.MaxUses,
            Status = entity.Status,
            TenantId = entity.TenantId,
            IsDeleted = entity.IsDeleted,
            CreatedAt = entity.CreatedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedBy
        };
    }

    /// <inheritdoc />
    public PromotionalCode ToEntity(PromotionalCodeDto dto)
    {
        var entity = new PromotionalCode()
        {
            Id = dto.Id,
            RemainingUses = dto.RemainingUses,
            MaxUses = dto.MaxUses,
            TenantId = dto.TenantId,
            UpdatedAt = dto.UpdatedAt,
            UpdatedBy = dto.UpdatedBy
        };
        
        if(!string.IsNullOrEmpty(dto.Name))
            entity.Name = dto.Name;
        if(!string.IsNullOrEmpty(dto.Code))
            entity.Code = dto.Code;
        if(dto.Status != null)
            entity.Status = (StatusEnum)dto.Status;
        if(dto.IsDeleted != null)
            entity.IsDeleted = (bool)dto.IsDeleted;
        if(dto.CreatedAt != null)
            entity.CreatedAt = (DateTime)dto.CreatedAt;
        if(!string.IsNullOrEmpty(dto.CreatedBy))
            entity.CreatedBy = dto.CreatedBy;
        
        return entity;
    }
}