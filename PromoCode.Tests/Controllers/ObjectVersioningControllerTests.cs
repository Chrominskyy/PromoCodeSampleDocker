using Microsoft.AspNetCore.Mvc;
using PromoCode.API.Controllers;
using PromoCode.Application.Services;
using PromoCode.Domain.Models;

namespace PromoCode.Tests.Controllers
{
    public class ObjectVersioningControllerTests
    {
        private readonly Mock<IObjectVersioningService> _mockService;
        private readonly ObjectVersioningController _controller;

        public ObjectVersioningControllerTests()
        {
            _mockService = new Mock<IObjectVersioningService>();
            _controller = new ObjectVersioningController(_mockService.Object);
        }

        [Fact]
        public async Task AddVersion_ReturnsOkResult()
        {
            // Arrange
            var version = CreateObjectVersioning();

            // Act
            var result = await _controller.AddVersion(version);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            _mockService.Verify(s => s.AddVersion(It.IsAny<ObjectVersioning>()), Times.Once);
        }

        [Fact]
        public async Task GetVersions_WithParams_ReturnsOkResult_WithVersions()
        {
            // Arrange
            var objectId = Guid.NewGuid();
            var objectType = "TestType";
            var objectTenant = Guid.NewGuid();
            var versions = new List<ObjectVersioning> { CreateObjectVersioning() };
            
            _mockService.Setup(s => s.GetVersions(objectType, objectTenant, objectId)).ReturnsAsync(versions);

            // Act
            var result = await _controller.GetVersions(objectId, objectType, objectTenant);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ObjectVersioning>>(okResult.Value);
            Assert.Equal(versions, returnValue);
            _mockService.Verify(s => s.GetVersions(objectType, objectTenant, objectId), Times.Once);
        }

        [Fact]
        public async Task GetVersions_WithoutParams_ReturnsOkResult_WithAllVersions()
        {
            // Arrange
            var versions = new List<ObjectVersioning> { CreateObjectVersioning() };
            _mockService.Setup(s => s.GetVersions()).ReturnsAsync(versions);

            // Act
            var result = await _controller.GetVersions();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ObjectVersioning>>(okResult.Value);
            Assert.Equal(versions, returnValue);
            _mockService.Verify(s => s.GetVersions(), Times.Once);
        }

        private ObjectVersioning CreateObjectVersioning()
        {
            return new ObjectVersioning
            {
                Id = Guid.NewGuid(),
                ObjectType = "TestType",
                ObjectTenant = Guid.NewGuid(),
                ObjectId = Guid.NewGuid(),
                UpdatedBy = "TestUser",
                UpdatedOn = DateTime.Now,
                BeforeValue =
                    "{\"Name \": \"TestCode \", \"Code \": \"Code1234 \", \"RemainingUses \": 255, \"MaxUses \": 255, \"Status \": \"ACTIVE \", \"Id \": \"eede8dee-4618-4f47-8c5e-800092ff2060 \", \"TenantId \": \"0fb61230-6ab0-4af9-aaa6-3bd18ecfdcf5 \", \"CreatedAt \": \"2024-05-16T15:37:35.951498 \", \"UpdatedAt \": null, \"CreatedBy \": \"Bartek \", \"UpdatedBy \": null, \"IsDeleted \": false}",
                AfterValue =
                    "{\"Name \": \"TestCode \", \"Code \": \"Code1234 \", \"RemainingUses \": 255, \"MaxUses \": 255, \"Status \": \"INACTIVE \", \"Id \": \"eede8dee-4618-4f47-8c5e-800092ff2060 \", \"TenantId \": \"0fb61230-6ab0-4af9-aaa6-3bd18ecfdcf5 \", \"CreatedAt \": \"2024-05-16T15:37:35.951498 \", \"UpdatedAt \": \"2024-05-17T15:37:35.951498\", \"CreatedBy \": \"Bartek \", \"UpdatedBy \": \"Michal\", \"IsDeleted \": false}"
            };
        }
    }
}
