using PromoCode.Application.Services;
using PromoCode.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PromoCode.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ObjectVersioningController : ControllerBase
{
    private readonly IObjectVersioningService _objectVersioningService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectVersioningController"/> class.
    /// </summary>
    /// <param name="objectVersioningService">The object versioning service.</param>
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
    [HttpGet("{objectId:guid}")]
    [SwaggerOperation(Summary = "Gets all versions for a specific object.")]
    [SwaggerResponse(200, "A collection of object versions.", typeof(IEnumerable<ObjectVersioning>))]
    public async Task<ActionResult<IEnumerable<ObjectVersioning>>> GetVersions(
        Guid objectId,
        [FromQuery] string objectType, 
        [FromQuery] Guid objectTenant
    )
    {
        var versions = await _objectVersioningService.GetVersions(objectType, objectTenant, objectId);
        return Ok(versions);
    }
    
    /// <summary>
    /// Gets all versions from the system.
    /// </summary>
    /// <returns>A collection of object versions.</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Gets all versions from the system.")]
    [SwaggerResponse(200, "A collection of object versions.", typeof(IEnumerable<ObjectVersioning>))]
    public async Task<ActionResult<IEnumerable<ObjectVersioning>>> GetVersions()
    {
        return Ok(await _objectVersioningService.GetVersions());
    }
}
