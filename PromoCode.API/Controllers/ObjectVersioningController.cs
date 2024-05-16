using PromoCode.Application.Services;
using PromoCode.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace PromoCode.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ObjectVersioningController : ControllerBase
{
    private readonly IObjectVersioningService _objectVersioningService;

    public ObjectVersioningController(IObjectVersioningService objectVersioningService)
    {
        _objectVersioningService = objectVersioningService;
    }

    /// <summary>
    /// Adds a new version for an object.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="objectId">ID of the object.</param>
    /// <param name="objectTenant">Tenant ID of the object.</param>
    /// <param name="beforeValue">Value before update.</param>
    /// <param name="afterValue">Value after update.</param>
    /// <param name="updatedBy">ID of the user who updated the object.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Adds a new version for an object.")]
    [SwaggerResponse(200, "The version was added successfully.")]
    public async Task<IActionResult> AddVersion(
        [FromQuery] string objectType, 
        [FromQuery] Guid objectId, 
        [FromQuery] Guid objectTenant, 
        [FromQuery] string? beforeValue, 
        [FromQuery] string? afterValue, 
        [FromQuery] string updatedBy)
    {
        await _objectVersioningService.AddVersion(objectType, objectId, objectTenant, beforeValue, afterValue, updatedBy);
        return Ok();
    }

    /// <summary>
    /// Gets all versions for a specific object.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="objectTenant">Tenant ID of the object.</param>
    /// <param name="objectId">ID of the object.</param>
    /// <returns>A collection of object versions.</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Gets all versions for a specific object.")]
    [SwaggerResponse(200, "A collection of object versions.", typeof(IEnumerable<ObjectVersioning>))]
    public async Task<ActionResult<IEnumerable<ObjectVersioning>>> GetVersions(
        [FromQuery] string objectType, 
        [FromQuery] Guid objectTenant, 
        [FromQuery] Guid objectId)
    {
        var versions = await _objectVersioningService.GetVersions(objectType, objectTenant, objectId);
        return Ok(versions);
    }

    /// <summary>
    /// Gets versions for a specific object by its ID.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="objectTenant">Tenant ID of the object.</param>
    /// <param name="objectId">ID of the object.</param>
    /// <returns>A collection of object versions.</returns>
    [HttpGet("{objectId:guid}")]
    [SwaggerOperation(Summary = "Gets versions for a specific object by its ID.")]
    [SwaggerResponse(200, "A collection of object versions.", typeof(IEnumerable<ObjectVersioning>))]
    [SwaggerResponse(501, "This functionality is not implemented.")]
    public async Task<ActionResult<IEnumerable<ObjectVersioning>>> GetVersionsByObjectId(
        [FromQuery] string objectType, 
        [FromQuery] Guid objectTenant, 
        [FromQuery] Guid objectId)
    {
        try
        {
            var versions = await _objectVersioningService.GetVersionsByObjectId(objectType, objectTenant, objectId);
            return Ok(versions);
        }
        catch (NotImplementedException)
        {
            return StatusCode(501, "This functionality is not implemented.");
        }
    }
}
