namespace PromoCode.API.Controllers;

using System.ComponentModel.DataAnnotations;
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
    /// <returns>
    /// Returns a collection of active promotional codes.
    /// </returns>
    /// <remarks>
    /// This method retrieves all active promotional codes from the database.
    /// </remarks>
    [HttpGet]
    [SwaggerOperation(Summary = "Gets all active promotional codes.")]
    [SwaggerResponse(200, "A collection of active promotional codes.", typeof(IEnumerable<PromotionalCodeDto>))]
    public async Task<ActionResult<IEnumerable<PromotionalCodeDto>>> GetPromotionalCodes()
    {
        return Ok(await _promotionalCodeService.GetActivePromotionalCodes());
    }
    
    /// <summary>
    /// Gets a specific promotional code by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the promotional code to retrieve.</param>
    /// <returns>
    /// Returns an ActionResult of type PromotionalCodeDto.
    /// If the promotional code is found, it returns Ok with the requested promotional code.
    /// If the promotional code is not found, it returns NotFound.
    /// </returns>
    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Gets a specific promotional code by its unique identifier.")]
    [SwaggerResponse(200, "The requested promotional code.", typeof(PromotionalCodeDto))]
    [SwaggerResponse(404, "Promotional code not found.")]
    public async Task<ActionResult<PromotionalCodeDto>> GetPromotionalCode(Guid id)
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
    /// <param name="promotionalCode">The promotional code to be created. This should be a valid PromotionalCodeDto object.</param>
    /// <returns>
    /// Returns an ActionResult of type Guid.
    /// If the promotional code is successfully created, it returns Ok with the unique identifier of the newly created promotional code.
    /// </returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Creates a new promotional code.")]
    [SwaggerResponse(201, "The unique identifier of the newly created promotional code.", typeof(Guid))]
    public async Task<ActionResult<Guid>> CreatePromotionalCode([FromBody] PromotionalCodeDto promotionalCode)
    {
        var userId = await _promotionalCodeService.CreatePromotionalCode(promotionalCode);
        return Ok(userId);
    }

    /// <summary>
    /// Updates an existing promotional code.
    /// </summary>
    /// <param name="promotionalCode">The updated promotional code <see cref="PromotionalCodeDto" />.</param>
    /// <returns>
    /// Returns an ActionResult of type PromotionalCodeDto.
    /// If the promotional code is successfully updated, it returns Ok with the updated promotional code.
    /// If the promotional code is not found, it returns NotFound.
    /// </returns>
    [HttpPut]
    [SwaggerOperation(Summary = "Updates an existing promotional code.")]
    [SwaggerResponse(200, "The updated promotional code.", typeof(PromotionalCodeDto))]
    [SwaggerResponse(404, "Promotional code not found.")]
    public async Task<ActionResult<PromotionalCodeDto>> UpdatePromotionalCode([FromBody] PromotionalCodeDto promotionalCode)
    {
        var updatedCode = await _promotionalCodeService.UpdatePromotionalCode(promotionalCode);
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
    /// <param name="updatedBy">The user who initiated the deletion.</param>
    /// <returns>
    /// Returns an ActionResult.
    /// If the promotional code is successfully deleted, it returns NoContent.
    /// If the promotional code is not found, it returns NotFound.
    /// </returns>
    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Deletes a promotional code by its unique identifier.")]
    [SwaggerResponse(204, "No content.")]
    [SwaggerResponse(404, "Promotional code not found.")]
    public async Task<ActionResult> DeletePromotionalCode(Guid id, [FromForm][Required] string updatedBy)
    {
        var result = await _promotionalCodeService.DeletePromotionalCode(id, updatedBy);
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
    /// <param name="updatedBy">The user who initiated the deactivation.</param>
    /// <returns>
    /// Returns an ActionResult.
    /// If the promotional code is successfully deactivated, it returns NoContent.
    /// If the promotional code is not found, it returns NotFound.
    /// </returns>
    [HttpPatch("{id:guid}/deactivate")]
    [SwaggerOperation(Summary = "Deactivates a promotional code.")]
    [SwaggerResponse(204, "No content.")]
    [SwaggerResponse(404, "Promotional code not found.")]
    public async Task<ActionResult> DeactivatePromotionalCode(Guid id, [FromForm][Required] string updatedBy)
    {
        var result = await _promotionalCodeService.DeactivatePromotionalCode(id, updatedBy);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
    
    /// <summary>
    /// Redeems a promotional code.
    /// </summary>
    /// <param name="code">The unique code of the promotional code to redeem.</param>
    /// <returns>
    /// Returns an ActionResult of type bool.
    /// If the promotional code is successfully redeemed, it returns Ok with true.
    /// If the promotional code could not be redeemed, it returns BadRequest.
    /// </returns>
    [HttpGet("{code}/redeem")]
    [SwaggerOperation(Summary = "Redeems a promotional code.")]
    [SwaggerResponse(200, "The promotional code was successfully redeemed.", typeof(bool))]
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
