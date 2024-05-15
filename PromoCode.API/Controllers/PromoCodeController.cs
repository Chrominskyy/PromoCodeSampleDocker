using Microsoft.AspNetCore.Mvc;
using PromoCode.Application.Services;
using PromoCode.Domain.Models;

namespace PromoCode.API.Controllers;

/// <summary>
/// Controller for managing promotional codes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PromoCodeController : ControllerBase
{
    private readonly IPromotionalCodeService _promotionalCodeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PromoCodeController"/> class.
    /// </summary>
    /// <param name="promotionalCodeService">The promotional code service.</param>
    public PromoCodeController(IPromotionalCodeService promotionalCodeService)
    {
        _promotionalCodeService = promotionalCodeService;
    }

    // GET api/promotionalcodes
    /// <summary>
    /// Gets all active promotional codes.
    /// </summary>
    /// <returns>A collection of active promotional codes.</returns>
    [HttpGet]
    public async Task<IEnumerable<PromotionalCode>> GetPromotionalCodes()
    {
        return await _promotionalCodeService.GetActivePromotionalCodes();
    }

    // GET api/promotionalcodes/5
    /// <summary>
    /// Gets a specific promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code.</param>
    /// <returns>The requested promotional code.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<PromotionalCode>> GetPromotionalCode(Guid id)
    {
        return await _promotionalCodeService.GetPromotionalCode(id);
    }

    // POST api/promotionalcodes
    /// <summary>
    /// Creates a new promotional code.
    /// </summary>
    /// <param name="promotionalCode">The promotional code to create.</param>
    /// <returns>The unique identifier of the newly created promotional code.</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreatePromotionalCode(PromotionalCode promotionalCode)
    {
        return await _promotionalCodeService.CreatePromotionalCode(promotionalCode);
    }

    // PUT api/promotionalcodes/5
    /// <summary>
    /// Updates an existing promotional code.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to update.</param>
    /// <param name="promotionalCode">The updated promotional code.</param>
    /// <returns>The updated promotional code.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<PromotionalCode>> UpdatePromotionalCode(int id, PromotionalCode promotionalCode)
    {
        return await _promotionalCodeService.UpdatePromotionalCode(id, promotionalCode);
    }

    // DELETE api/promotionalcodes/5
    /// <summary>
    /// Deletes a promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePromotionalCode(Guid id)
    {
        await _promotionalCodeService.DeletePromotionalCode(id);
        return NoContent();
    }

    // GET api/promotionalcodes/5/redeem
    /// <summary>
    /// Redeems a promotional code.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to redeem.</param>
    /// <returns>The redeemed promotional code.</returns>
    [HttpGet("{id}/redeem")]
    public async Task<ActionResult<PromotionalCode>> RedeemPromotionalCode(Guid id)
    {
        return await _promotionalCodeService.RedeemPromotionalCode(id);
    }

    // PATCH api/promotionalcodes/5/deactivate
    /// <summary>
    /// Deactivates a promotional code.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to deactivate.</param>
    /// <returns>The deactivated promotional code.</returns>
    [HttpPatch("{id}/deactivate")]
    public async Task<ActionResult<PromotionalCode>> DeactivatePromotionalCode(Guid id)
    {
        return await _promotionalCodeService.DeactivatePromotionalCode(id);
    }
}