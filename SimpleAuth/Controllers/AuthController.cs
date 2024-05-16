using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Models;
using SimpleAuth.Services;

namespace SimpleAuth.Controllers;

/// <summary>
/// Controller for handling authentication and user management.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="authService">The authentication service.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Authenticates a user with the provided credentials.
    /// </summary>
    /// <param name="request">The login request containing username and password.</param>
    /// <returns>An <see cref="IActionResult"/> with the authentication token if successful, otherwise Unauthorized.</returns>
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
    {
        var token = await _authService.AuthenticateAsync(request.Username, request.Password);
        if (token == null)
        {
            return Unauthorized();
        }
        return Ok(new { Token = token });
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="user">The user object to register.</param>
    /// <returns>An <see cref="IActionResult"/> with the created user and its ID.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        var userId = await _authService.RegisterUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new { userId }, user);
    }

    /// <summary>
    /// Deactivates a user by its ID.
    /// </summary>
    /// <param name="userId">The ID of the user to deactivate.</param>
    /// <returns>An <see cref="IActionResult"/> with NoContent if successful, otherwise NotFound.</returns>
    [HttpPost("{userId:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid userId)
    {
        var result = await _authService.DeactivateUserAsync(userId);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    /// <summary>
    /// Deletes a user by its ID.
    /// </summary>
    /// <param name="userId">The ID of the user to delete.</param>
    /// <returns>An <see cref="IActionResult"/> with NoContent if successful, otherwise NotFound.</returns>
    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> Delete(Guid userId)
    {
        var result = await _authService.DeleteUserAsync(userId);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    /// <summary>
    /// Retrieves a user by its ID.
    /// </summary>
    /// <param name="userId">The ID of the user to retrieve.</param>
    /// <returns>An <see cref="IActionResult"/> with the user if found, otherwise NotFound.</returns>
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var user = await _authService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    /// <summary>
    /// Updates a user.
    /// </summary>
    /// <param name="user">The updated user object.</param>
    /// <returns>An <see cref="IActionResult"/> with the updated user.</returns>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] User user)
    {
        var updatedUser = await _authService.UpdateUserAsync(user);
        return Ok(updatedUser);
    }
    
    
    /// <summary>
    /// Validates the provided authentication token.
    /// </summary>
    /// <param name="token">The authentication token to validate.</param>
    /// <returns>An <see cref="IActionResult"/> with Ok status if the token is valid, otherwise Unauthorized.</returns>
    [HttpGet]
    public async Task<IActionResult> ValidateToken(string token)
    {
        var valid = _authService.ValidateToken(token);

        if (valid)
        {
            return Ok();
        }

        return Unauthorized();
    }
}