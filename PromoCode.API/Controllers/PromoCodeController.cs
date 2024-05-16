namespace PromoCode.API.Controllers;

using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller for managing promotional codes.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/PromotionalCode")]
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

    /// <summary>
    /// Gets all active promotional codes.
    /// </summary>
    /// <returns>A collection of active promotional codes.</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Gets all active promotional codes.")]
    [SwaggerResponse(200, "A collection of active promotional codes.", typeof(IEnumerable<PromotionalCode>))]
    public async Task<IEnumerable<PromotionalCode>> GetPromotionalCodes()
    {
        return await _promotionalCodeService.GetActivePromotionalCodes();
    }

    /// <summary>
    /// Gets a specific promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code.</param>
    /// <returns>The requested promotional code.</returns>
    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Gets a specific promotional code by its unique identifier.")]
    [SwaggerResponse(200, "The requested promotional code.", typeof(PromotionalCode))]
    [SwaggerResponse(404, "Promotional code not found.")]
    public async Task<ActionResult<PromotionalCode>> GetPromotionalCode(Guid id)
    {
        var promotionalCode = await _promotionalCodeService.GetPromotionalCode(id);
        if (promotionalCode == null)
        {
            return NotFound();
        }
        return Ok(promotionalCode);
    }

    /// <summary>
    /// Creates a new promotional code.
    /// </summary>
    /// <param name="promotionalCode">The promotional code to create.</param>
    /// <returns>The unique identifier of the newly created promotional code.</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Creates a new promotional code.")]
    [SwaggerResponse(201, "The unique identifier of the newly created promotional code.", typeof(Guid))]
    public async Task<ActionResult<Guid>> CreatePromotionalCode([FromBody] PromotionalCode promotionalCode)
    {
        var userId = await _promotionalCodeService.CreatePromotionalCode(promotionalCode);
        return CreatedAtAction(nameof(GetPromotionalCode), new { id = userId }, promotionalCode);
    }

    /// <summary>
    /// Updates an existing promotional code.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to update.</param>
    /// <param name="promotionalCode">The updated promotional code.</param>
    /// <returns>The updated promotional code.</returns>
    [HttpPut("{id:guid}")]
    [SwaggerOperation(Summary = "Updates an existing promotional code.")]
    [SwaggerResponse(200, "The updated promotional code.", typeof(PromotionalCode))]
    [SwaggerResponse(404, "Promotional code not found.")]
    public async Task<ActionResult<PromotionalCode>> UpdatePromotionalCode(Guid id, [FromBody] PromotionalCode promotionalCode)
    {
        var updatedCode = await _promotionalCodeService.UpdatePromotionalCode(id, promotionalCode);
        if (updatedCode == null)
        {
            return NotFound();
        }
        return Ok(updatedCode);
    }

    /// <summary>
    /// Deletes a promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Deletes a promotional code by its unique identifier.")]
    [SwaggerResponse(204, "No content.")]
    [SwaggerResponse(404, "Promotional code not found.")]
    public async Task<ActionResult> DeletePromotionalCode(Guid id)
    {
        var result = await _promotionalCodeService.DeletePromotionalCode(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    /// <summary>
    /// Deactivates a promotional code.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to deactivate.</param>
    /// <returns>No content if the promotional code was deactivated, otherwise Not Found.</returns>
    [HttpPatch("{id:guid}/deactivate")]
    [SwaggerOperation(Summary = "Deactivates a promotional code.")]
    [SwaggerResponse(204, "No content.")]
    [SwaggerResponse(404, "Promotional code not found.")]
    public async Task<ActionResult> DeactivatePromotionalCode(Guid id)
    {
        var result = await _promotionalCodeService.DeactivatePromotionalCode(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    /// <summary>
    /// Redeems a promotional code.
    /// </summary>
    /// <param name="code">The promotional code to redeem.</param>
    /// <returns>The redeemed promotional code.</returns>
    [HttpGet("{code}/redeem")]
    [SwaggerOperation(Summary = "Redeems a promotional code.")]
    [SwaggerResponse(200, "The promotional code was successfully redeemed.")]
    [SwaggerResponse(400, "The promotional code could not be redeemed.")]
    public async Task<ActionResult<bool>> RedeemPromotionalCode(string code)
    {
        var result = await _promotionalCodeService.RedeemPromotionalCode(code);
        if (result)
        {
            return Ok(true);
        }
        return BadRequest();
    }
}
