using Microsoft.AspNetCore.Mvc;
using PromoCode.API.Controllers;
using PromoCode.Application.Services;
using PromoCode.Domain.Enums;
using PromoCode.Domain.Models;

namespace PromoCode.Tests.Controllers
{
    public class PromoCodeControllerTests
    {
        private readonly PromoCodeController _controller;
        private readonly Mock<IPromotionalCodeService> _serviceMock;

        public PromoCodeControllerTests()
        {
            _serviceMock = new Mock<IPromotionalCodeService>();
            _controller = new PromoCodeController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetPromotionalCodes_ReturnsOkResult_WithListOfPromotionalCodes()
        {
            // Arrange
            var promotionalCodes = new List<PromotionalCodeDto>
            {
                CreatePromotionalCodeDto()
            };
            _serviceMock.Setup(service => service.GetActivePromotionalCodes())
                        .ReturnsAsync(promotionalCodes);

            // Act
            var result = await _controller.GetPromotionalCodes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<PromotionalCodeDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetPromotionalCode_ReturnsOkResult_WithPromotionalCode()
        {
            // Arrange
            var promoId = Guid.NewGuid();
            var promotionalCode = CreatePromotionalCodeDto();
            promotionalCode.Id = promoId;
            _serviceMock.Setup(service => service.GetPromotionalCode(promoId))
                        .ReturnsAsync(promotionalCode);

            // Act
            var result = await _controller.GetPromotionalCode(promoId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PromotionalCodeDto>(okResult.Value);
            Assert.Equal(promoId, returnValue.Id);
        }

        [Fact]
        public async Task GetPromotionalCode_ReturnsNotFound_WhenPromotionalCodeNotFound()
        {
            // Arrange
            var promoId = Guid.NewGuid();
            _serviceMock.Setup(service => service.GetPromotionalCode(promoId))
                        .ReturnsAsync((PromotionalCodeDto)null);

            // Act
            var result = await _controller.GetPromotionalCode(promoId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreatePromotionalCode_ReturnsCreatedAtAction_WithNewlyCreatedPromotionalCode()
        {
            // Arrange
            var promotionalCode = CreatePromotionalCodeDto();
            var newId = Guid.NewGuid();
            _serviceMock.Setup(service => service.CreatePromotionalCode(promotionalCode))
                        .ReturnsAsync(newId);

            // Act
            var result = await _controller.CreatePromotionalCode(promotionalCode);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(newId, okResult.Value);
        }

        [Fact]
        public async Task UpdatePromotionalCode_ReturnsOkResult_WithUpdatedPromotionalCode()
        {
            // Arrange
            var promoId = Guid.NewGuid();
            var promotionalCode = CreatePromotionalCodeDto();
            promotionalCode.Id = promoId;
            _serviceMock.Setup(service => service.UpdatePromotionalCode(promotionalCode))
                        .ReturnsAsync(promotionalCode);

            // Act
            var result = await _controller.UpdatePromotionalCode(promotionalCode);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PromotionalCodeDto>(okResult.Value);
            Assert.Equal(promoId, returnValue.Id);
        }

        [Fact]
        public async Task UpdatePromotionalCode_ReturnsNotFound_WhenPromotionalCodeNotFound()
        {
            // Arrange
            var promotionalCode = CreatePromotionalCodeDto();
            _serviceMock.Setup(service => service.UpdatePromotionalCode(promotionalCode))
                        .ReturnsAsync((PromotionalCodeDto)null);

            // Act
            var result = await _controller.UpdatePromotionalCode(promotionalCode);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task DeletePromotionalCode_ReturnsNoContent_WhenPromotionalCodeDeleted()
        {
            // Arrange
            var promoId = Guid.NewGuid();
            _serviceMock.Setup(service => service.DeletePromotionalCode(promoId, "testUser"))
                        .ReturnsAsync(true);

            // Act
            var result = await _controller.DeletePromotionalCode(promoId, "testUser");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeletePromotionalCode_ReturnsNotFound_WhenPromotionalCodeNotFound()
        {
            // Arrange
            var promoId = Guid.NewGuid();
            _serviceMock.Setup(service => service.DeletePromotionalCode(promoId, "testUser"))
                        .ReturnsAsync(false);

            // Act
            var result = await _controller.DeletePromotionalCode(promoId, "testUser");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task RedeemPromotionalCode_ReturnsOk_WhenRedemptionIsSuccessful()
        {
            // Arrange
            var promoCode = "CODE1";
            _serviceMock.Setup(service => service.RedeemPromotionalCode(promoCode))
                        .ReturnsAsync(true);

            // Act
            var result = await _controller.RedeemPromotionalCode(promoCode);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task RedeemPromotionalCode_ReturnsBadRequest_WhenRedemptionFails()
        {
            // Arrange
            var promoCode = "CODE1";
            _serviceMock.Setup(service => service.RedeemPromotionalCode(promoCode))
                        .ReturnsAsync(false);

            // Act
            var result = await _controller.RedeemPromotionalCode(promoCode);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task DeactivatePromotionalCode_ReturnsNoContent_WhenDeactivationIsSuccessful()
        {
            // Arrange
            var promoId = Guid.NewGuid();
            _serviceMock.Setup(service => service.DeactivatePromotionalCode(promoId, "testUser"))
                        .ReturnsAsync(true);

            // Act
            var result = await _controller.DeactivatePromotionalCode(promoId, "testUser");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeactivatePromotionalCode_ReturnsNotFound_WhenPromotionalCodeNotFound()
        {
            // Arrange
            var promoId = Guid.NewGuid();
            _serviceMock.Setup(service => service.DeactivatePromotionalCode(promoId, "testUser"))
                        .ReturnsAsync(false);

            // Act
            var result = await _controller.DeactivatePromotionalCode(promoId, "testUser");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private PromotionalCodeDto CreatePromotionalCodeDto()
        {
            return new PromotionalCodeDto()
            {
                Id = Guid.NewGuid(),
                Code = "CODE1",
                CreatedBy = "USER1",
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                TenantId = Guid.NewGuid(),
                Name = "NAME1",
                Status = StatusEnum.Active,
                MaxUses = 10,
                RemainingUses = 10
            };
        }
    }
}
