using Chrominsky.Utils.Mappers.Base;
using Chrominsky.Utils.Services;
using PromoCode.Application.Services;
using PromoCode.Domain.Enums;
using PromoCode.Domain.Mappers;
using PromoCode.Domain.Models;
using PromoCode.Infrastructure.Repositories;

namespace PromoCode.Tests.Services
{
    public class PromotionalCodeServiceTests
    {
        private readonly Mock<ICacheService> _mockCacheService;
        private readonly Mock<IPromotionalCodeRepository> _mockRepository;
        private readonly Mock<IBaseMapper<PromotionalCode, PromotionalCodeDto>> _mockMapper;
        private readonly PromotionalCodeService _service;

        public PromotionalCodeServiceTests()
        {
            _mockCacheService = new Mock<ICacheService>();
            _mockRepository = new Mock<IPromotionalCodeRepository>();
            _mockMapper = new Mock<IBaseMapper<PromotionalCode, PromotionalCodeDto>>();
            _service = new PromotionalCodeService(_mockRepository.Object, _mockCacheService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetPromotionalCode_ReturnsDto_WhenCodeExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var promotionalCode = new PromotionalCode { Id = id, Code = "PROMO123" };
            var promotionalCodeDto = new PromotionalCodeDto { Id = id, Code = "PROMO123" };

            _mockCacheService
                .Setup(s => s.GetOrAddAsync(It.IsAny<string>(), It.IsAny<Func<Task<PromotionalCode>>>(), null))
                .ReturnsAsync(promotionalCode);
            _mockMapper
                .Setup(m => m.ToDto(promotionalCode))
                .Returns(promotionalCodeDto);

            // Act
            var result = await _service.GetPromotionalCode(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(promotionalCodeDto, result);
        }

        [Fact]
        public async Task GetPromotionalCode_ReturnsNull_WhenCodeDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockCacheService
                .Setup(s => s.GetOrAddAsync(It.IsAny<string>(), It.IsAny<Func<Task<PromotionalCode>>>(), null))
                .ReturnsAsync((PromotionalCode)null);

            // Act
            var result = await _service.GetPromotionalCode(id);

            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public async Task GetActivePromotionalCodes_ReturnsEmptyList_WhenRepositoryReturnsNull()
        {
            // Arrange
            var mockPromotionalCodeRepository = new Mock<IPromotionalCodeRepository>();
            var mockCacheService = new Mock<ICacheService>();
            var baseMapper = new Mock<IBaseMapper<PromotionalCode, PromotionalCodeDto>>();
            var mockReturn = new List<PromotionalCode>();

            mockPromotionalCodeRepository.Setup(x => x.GetPromotionalCodesAsync())
               .ReturnsAsync(mockReturn);

            var service = new PromotionalCodeService(
                mockPromotionalCodeRepository.Object,
                mockCacheService.Object,
                baseMapper.Object
            );

            // Act
            var result = await service.GetActivePromotionalCodes();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetActivePromotionalCodes_ReturnsMappedList_WhenRepositoryReturnsData()
        {
            // Arrange
            var mockPromotionalCodeRepository = new Mock<IPromotionalCodeRepository>();
            var mockCacheService = new Mock<ICacheService>();
            var baseMapper = new Mock<IBaseMapper<PromotionalCode, PromotionalCodeDto>>();

            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            
            var promotionalCodes = new List<PromotionalCode>
            {
                new() { Id = guid1, Code = "ABC123" },
                new() { Id = guid2, Code = "DEF456" }
            };

            var promotionalCodeDtos = new List<PromotionalCodeDto>
            {
                new() { Id = guid1, Code = "ABC123" },
                new() { Id = guid2, Code = "DEF456" }
            };

            mockPromotionalCodeRepository.Setup(x => x.GetPromotionalCodesAsync())
               .ReturnsAsync(promotionalCodes);

            baseMapper.Setup(x => x.ToDto(It.IsAny<PromotionalCode>()))
               .Returns((PromotionalCode pc) => promotionalCodeDtos.FirstOrDefault(dto => dto.Id == pc.Id));

            var service = new PromotionalCodeService(
                mockPromotionalCodeRepository.Object,
                mockCacheService.Object,
                baseMapper.Object
            );

            // Act
            var result = await service.GetActivePromotionalCodes();

            // Assert
            Assert.Equal(promotionalCodeDtos, result);
        }

        [Fact]
        public async Task CreatePromotionalCode_ReturnsGuid()
        {
            // Arrange
            var promotionalCodeDto = new PromotionalCodeDto { Id = Guid.NewGuid(), Code = "PROMO123" };
            var promotionalCode = new PromotionalCode { Id = promotionalCodeDto.Id, Code = promotionalCodeDto.Code };
            var expectedGuid = promotionalCodeDto.Id;

            _mockMapper
                .Setup(m => m.ToEntity(promotionalCodeDto))
                .Returns(promotionalCode);
            _mockRepository
                .Setup(r => r.AddPromotionalCodeAsync(promotionalCode))
                .ReturnsAsync(expectedGuid);

            // Act
            var result = await _service.CreatePromotionalCode(promotionalCodeDto);

            // Assert
            Assert.Equal(expectedGuid, result);
            _mockMapper.Verify(m => m.ToEntity(promotionalCodeDto), Times.Once);
            _mockRepository.Verify(r => r.AddPromotionalCodeAsync(promotionalCode), Times.Once);
        }

        [Fact]
        public async Task UpdatePromotionalCode_ReturnsUpdatedDto()
        {
            // Arrange
            var promotionalCodeDto = new PromotionalCodeDto { Id = Guid.NewGuid(), Code = "PROMO123" };
            var promotionalCode = new PromotionalCode { Id = promotionalCodeDto.Id, Code = promotionalCodeDto.Code };
            var updatedPromotionalCode = new PromotionalCode { Id = promotionalCodeDto.Id, Code = "UPDATED123" };
            var updatedPromotionalCodeDto = new PromotionalCodeDto { Id = promotionalCodeDto.Id, Code = "UPDATED123" };

            _mockMapper
                .Setup(m => m.ToEntity(promotionalCodeDto))
                .Returns(promotionalCode);
            _mockRepository
                .Setup(r => r.UpdatePromotionalCodeAsync(promotionalCode))
                .ReturnsAsync(updatedPromotionalCode);
            _mockMapper
                .Setup(m => m.ToDto(updatedPromotionalCode))
                .Returns(updatedPromotionalCodeDto);

            // Act
            var result = await _service.UpdatePromotionalCode(promotionalCodeDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedPromotionalCodeDto, result);
            _mockMapper.Verify(m => m.ToEntity(promotionalCodeDto), Times.Once);
            _mockRepository.Verify(r => r.UpdatePromotionalCodeAsync(promotionalCode), Times.Once);
            _mockMapper.Verify(m => m.ToDto(updatedPromotionalCode), Times.Once);
        }

        [Fact]
        public async Task DeletePromotionalCode_ReturnsTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updatedBy = "testUser";

            _mockRepository
                .Setup(r => r.DeletePromotionalCodeAsync(id, updatedBy))
                .Returns(Task.CompletedTask);
            _mockCacheService
                .Setup(s => s.RemoveAsync(id.ToString()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeletePromotionalCode(id, updatedBy);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(r => r.DeletePromotionalCodeAsync(id, updatedBy), Times.Once);
            _mockCacheService.Verify(s => s.RemoveAsync(id.ToString()), Times.Once);
        }
        
        [Fact]
        public async Task RedeemPromotionalCode_CodeNotFound_ReturnsFalse()
        {
            // Arrange
            var code = "PROMO123";
            _mockRepository
                .Setup(r => r.GetPromotionalCodeByCodeAsync(code))
                .ReturnsAsync((PromotionalCode)null);

            // Act
            var result = await _service.RedeemPromotionalCode(code);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(r => r.GetPromotionalCodeByCodeAsync(code), Times.Once);
        }

        [Fact]
        public async Task RedeemPromotionalCode_CodeRemainingUsesLessThanOne_ReturnsFalse()
        {
            // Arrange
            var code = "PROMO123";
            var promoCode = new PromotionalCode { Code = code, RemainingUses = 0 };
            _mockRepository
                .Setup(r => r.GetPromotionalCodeByCodeAsync(code))
                .ReturnsAsync(promoCode);

            // Act
            var result = await _service.RedeemPromotionalCode(code);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(r => r.GetPromotionalCodeByCodeAsync(code), Times.Once);
        }

        [Fact]
        public async Task RedeemPromotionalCode_ValidCode_ReturnsTrue()
        {
            // Arrange
            var code = "PROMO123";
            var promoCode = new PromotionalCode { Id = Guid.NewGuid(), Code = code, RemainingUses = 1 };
            var promoCodeRet = promoCode;
            _mockRepository
                .Setup(r => r.GetPromotionalCodeByCodeAsync(code))
                .ReturnsAsync(promoCode);
            _mockRepository
                .Setup(r => r.UpdatePromotionalCodeAsync(It.IsAny<PromotionalCode>()))
                .ReturnsAsync(promoCodeRet);

            // Act
            var result = await _service.RedeemPromotionalCode(code);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(r => r.GetPromotionalCodeByCodeAsync(code), Times.Once);
            _mockRepository.Verify(r => r.UpdatePromotionalCodeAsync(It.Is<PromotionalCode>(pc => pc.RemainingUses == 0 && pc.Id == promoCode.Id)), Times.Once);
        }

        [Fact]
        public async Task DeactivatePromotionalCode_CodeNotFound_ReturnsFalse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updatedBy = "testUser";
            _mockRepository
                .Setup(r => r.GetPromotionalCodeByIdAsync(id))
                .ReturnsAsync((PromotionalCode?)null);

            // Act
            var result = await _service.DeactivatePromotionalCode(id, updatedBy);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(r => r.GetPromotionalCodeByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeactivatePromotionalCode_ValidCode_ReturnsTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updatedBy = "testUser";
            var promoCode = new PromotionalCode { Id = id, Status = StatusEnum.Active };
            _mockRepository
                .Setup(r => r.GetPromotionalCodeByIdAsync(id))
                .ReturnsAsync(promoCode);
            _mockRepository
                .Setup(r => r.UpdatePromotionalCodeAsync(It.IsAny<PromotionalCode>()))
                .ReturnsAsync(promoCode);

            // Act
            var result = await _service.DeactivatePromotionalCode(id, updatedBy);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(r => r.GetPromotionalCodeByIdAsync(id), Times.Once);
            _mockRepository.Verify(
                r => r.UpdatePromotionalCodeAsync(It.Is<PromotionalCode>(pc =>
                    pc.Status == StatusEnum.Inactive && pc.Id == id && pc.UpdatedBy == updatedBy)), Times.Once);
        }
    }
}