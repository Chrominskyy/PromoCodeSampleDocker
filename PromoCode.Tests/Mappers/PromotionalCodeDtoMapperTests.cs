using PromoCode.Domain.Enums;
using PromoCode.Domain.Mappers;
using PromoCode.Domain.Models;

namespace PromoCode.Tests.Mappers;

public class PromotionalCodeDtoMapperTests
{
    private readonly PromotionalCodeDtoMapper _mapper;

        public PromotionalCodeDtoMapperTests()
        {
            _mapper = new PromotionalCodeDtoMapper();
        }

        [Fact]
        public void ToDto_ConvertsEntityToDto()
        {
            // Arrange
            var entity = new PromotionalCode
            {
                Id = Guid.NewGuid(),
                Name = "Promo 1",
                Code = "PROMO1",
                RemainingUses = 5,
                MaxUses = 10,
                Status = StatusEnum.Active,
                TenantId = Guid.NewGuid(),
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "user1",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "user2"
            };

            // Act
            var dto = _mapper.ToDto(entity);

            // Assert
            Assert.Equal(entity.Id, dto.Id);
            Assert.Equal(entity.Name, dto.Name);
            Assert.Equal(entity.Code, dto.Code);
            Assert.Equal(entity.RemainingUses, dto.RemainingUses);
            Assert.Equal(entity.MaxUses, dto.MaxUses);
            Assert.Equal(entity.Status, dto.Status);
            Assert.Equal(entity.TenantId, dto.TenantId);
            Assert.Equal(entity.IsDeleted, dto.IsDeleted);
            Assert.Equal(entity.CreatedAt, dto.CreatedAt);
            Assert.Equal(entity.CreatedBy, dto.CreatedBy);
            Assert.Equal(entity.UpdatedAt, dto.UpdatedAt);
            Assert.Equal(entity.UpdatedBy, dto.UpdatedBy);
        }

        [Fact]
        public void ToEntity_ConvertsDtoToEntity()
        {
            // Arrange
            var dto = new PromotionalCodeDto
            {
                Id = Guid.NewGuid(),
                Name = "Promo 1",
                Code = "PROMO1",
                RemainingUses = 5,
                MaxUses = 10,
                Status = StatusEnum.Active,
                TenantId = Guid.NewGuid(),
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "user1",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "user2"
            };

            // Act
            var entity = _mapper.ToEntity(dto);

            // Assert
            Assert.Equal(dto.Id, entity.Id);
            Assert.Equal(dto.Name, entity.Name);
            Assert.Equal(dto.Code, entity.Code);
            Assert.Equal(dto.RemainingUses, entity.RemainingUses);
            Assert.Equal(dto.MaxUses, entity.MaxUses);
            Assert.Equal(dto.Status, entity.Status);
            Assert.Equal(dto.TenantId, entity.TenantId);
            Assert.Equal(dto.IsDeleted, entity.IsDeleted);
            Assert.Equal(dto.CreatedAt, entity.CreatedAt);
            Assert.Equal(dto.CreatedBy, entity.CreatedBy);
            Assert.Equal(dto.UpdatedAt, entity.UpdatedAt);
            Assert.Equal(dto.UpdatedBy, entity.UpdatedBy);
        }

        [Fact]
        public void ToEntity_HandlesNullAndEmptyFields()
        {
            // Arrange
            var dto = new PromotionalCodeDto
            {
                Id = Guid.NewGuid(),
                RemainingUses = 5,
                MaxUses = 10,
                TenantId = Guid.NewGuid(),
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "user2",
                Status = StatusEnum.Active,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "user1",
            };

            // Act
            var entity = _mapper.ToEntity(dto);

            // Assert
            Assert.Equal(dto.Id, entity.Id);
            Assert.Equal(dto.RemainingUses, entity.RemainingUses);
            Assert.Equal(dto.MaxUses, entity.MaxUses);
            Assert.Equal(dto.TenantId, entity.TenantId);
            Assert.Equal(dto.UpdatedAt, entity.UpdatedAt);
            Assert.Equal(dto.UpdatedBy, entity.UpdatedBy);
            Assert.Null(entity.Name);
            Assert.Null(entity.Code);
            Assert.Equal(dto.Status, entity.Status);
            Assert.Equal(false, entity.IsDeleted);
            Assert.Equal(dto.CreatedAt, entity.CreatedAt);
            Assert.Equal(dto.CreatedBy, entity.CreatedBy);
        }
}