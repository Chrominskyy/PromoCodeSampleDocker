using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PromoCode.Domain.Enums;
using PromoCode.Domain.Models;
using PromoCode.Infrastructure.Contexts;
using PromoCode.Infrastructure.Repositories;

namespace PromoCode.Tests.Repositories;

public class PromotionalCodeRepositoryTests
{
    private readonly PromoCodeDbContext _context;
    private readonly PromotionalCodeRepository _repository;
    private readonly Mock<IObjectVersioningRepository> _objectVersioningRepository;

    public PromotionalCodeRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<PromoCodeDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new PromoCodeDbContext(options);
        _objectVersioningRepository = new Mock<IObjectVersioningRepository>();
        _repository = new PromotionalCodeRepository(_context, _objectVersioningRepository.Object);
    }

    [Fact]
    public async Task GetPromotionalCodesAsync_ReturnsActiveCodes()
    {
        // Arrange
        _context.PromotionalCodes.AddRange(
            new PromotionalCode { Code = "PROMO1", Name = "Promo 1", MaxUses = 10, RemainingUses = 5, Status = StatusEnum.Active, IsDeleted = false, UpdatedAt = DateTime.UtcNow, CreatedBy = "user1", TenantId = Guid.NewGuid() },
            new PromotionalCode { Code = "PROMO2", Name = "Promo 2", MaxUses = 10, RemainingUses = 5, Status = StatusEnum.Inactive, IsDeleted = false, UpdatedAt = DateTime.UtcNow, CreatedBy = "user1", TenantId = Guid.NewGuid() },
            new PromotionalCode { Code = "PROMO3", Name = "Promo 3", MaxUses = 10, RemainingUses = 5, Status = StatusEnum.Active, IsDeleted = true, UpdatedAt = DateTime.UtcNow, CreatedBy = "user1", TenantId = Guid.NewGuid() }
        );
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetPromotionalCodesAsync();

        // Assert
        Assert.Equal(1, result.Count());
        Assert.All(result, code => Assert.Equal(StatusEnum.Active, code.Status));
    }

    [Fact]
    public async Task GetPromotionalCodeByIdAsync_ReturnsCode_WhenCodeExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var code = new PromotionalCode { Id = id, Code = "PROMO1", Name = "Promo 1", MaxUses = 10, RemainingUses = 5, Status = StatusEnum.Active, IsDeleted = false, CreatedBy = "user1", TenantId = Guid.NewGuid() };
        _context.PromotionalCodes.Add(code);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetPromotionalCodeByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public async Task GetPromotionalCodeByIdAsync_ReturnsNull_WhenCodeDoesNotExist()
    {
        // Act
        var result = await _repository.GetPromotionalCodeByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPromotionalCodeByCodeAsync_ReturnsCode_WhenCodeExists()
    {
        // Arrange
        var code = "PROMO123";
        var promotionalCode = new PromotionalCode { Code = code, Name = "Promo 123", MaxUses = 10, RemainingUses = 5, Status = StatusEnum.Active, IsDeleted = false, CreatedBy = "user1", TenantId = Guid.NewGuid() };
        _context.PromotionalCodes.Add(promotionalCode);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetPromotionalCodeByCodeAsync(code);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(code, result.Code);
    }

    [Fact]
    public async Task GetPromotionalCodeByCodeAsync_ReturnsNull_WhenCodeDoesNotExist()
    {
        // Act
        var result = await _repository.GetPromotionalCodeByCodeAsync("PROMO123");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddPromotionalCodeAsync_AddsAndReturnsId()
    {
        // Arrange
        var promotionalCode = new PromotionalCode { Code = "PROMO1", Name = "Promo 1", MaxUses = 10, RemainingUses = 10, TenantId = Guid.NewGuid(), CreatedBy = "testUser" };

        // Act
        var result = await _repository.AddPromotionalCodeAsync(promotionalCode);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        var addedCode = await _context.PromotionalCodes.FindAsync(result);
        Assert.NotNull(addedCode);
    }

    [Fact]
    public async Task UpdatePromotionalCodeAsync_UpdatesAndReturnsCode()
    {
        // Arrange
        var promotionalCode = new PromotionalCode { Id = Guid.NewGuid(), Code = "PROMO1", Name = "Promo 1", MaxUses = 10, RemainingUses = 10, TenantId = Guid.NewGuid(), UpdatedBy = "testUser", CreatedBy = "testUser" };
        _context.PromotionalCodes.Add(promotionalCode);
        await _context.SaveChangesAsync();

        promotionalCode.Status = StatusEnum.Inactive;

        // Act
        var result = await _repository.UpdatePromotionalCodeAsync(promotionalCode);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(StatusEnum.Inactive, result.Status);
    }

    [Fact]
    public async Task DeletePromotionalCodeAsync_SoftDeletesCode()
    {
        // Arrange
        var id = Guid.NewGuid();
        var promotionalCode = new PromotionalCode { Id = id, Code = "PROMO1", Name = "Promo 1", MaxUses = 10, RemainingUses = 5, TenantId = Guid.NewGuid(), CreatedBy = "testUser" };
        _context.PromotionalCodes.Add(promotionalCode);
        await _context.SaveChangesAsync();

        // Act
        await _repository.DeletePromotionalCodeAsync(id, "testUser");

        // Assert
        var deletedCode = await _context.PromotionalCodes.FindAsync(id);
        Assert.NotNull(deletedCode);
        Assert.True(deletedCode.IsDeleted);
        Assert.Equal(StatusEnum.Deleted, deletedCode.Status);
    }

    [Fact]
    public async Task CheckCodeAvailability_ReturnsRemainingUses()
    {
        // Arrange
        var code = "PROMO123";
        var promotionalCode = new PromotionalCode { Code = code, Name = "Promo 123", MaxUses = 10, RemainingUses = 5, Status = StatusEnum.Active, IsDeleted = false, CreatedBy = "user1", TenantId = Guid.NewGuid() };
        _context.PromotionalCodes.Add(promotionalCode);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.CheckCodeAvailability(code);

        // Assert
        Assert.Equal(5, result);
    }
}